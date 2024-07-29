# WebSecurity

This project is a series of Proofs of Concept (POCs) that implements comprehensive web security measures using the .NET framework. Starting with basic authentication methods like Cookie based authentication and JWT authentication, then explores multiple authentication schemes in .Net. Applies Permission-based access control. And implements OAuth/OpenIdConnect for authentication and authorization.

### Notes:
1. This project uses .Net 6.
2. Each project has its own section for explaning its purpose and what have been used in it, and what was used as external libraries.
3. If you want to run a project, then check its details section if it uses a DB and if so, make sure to apply migrations before running the project.
4. For `OAuth` and `OpenIdConnect` projects, you have to provide your own the **ClientId** and **ClientSecret** for your registered applications.
5. For `OAuth` and `OpenIdConnect` projects, don't try to start the flow by hitting the dedicated endpoints using **Swagger** endpoints you will hit error, instead open a tab and specify the full path for the endpoints.   

## **CookieAuth**

* Implements Cookie-based authentication in .Net.

* Exploits the built-in functionality in .Net to achieve such an authentication measure, no external libraries were used.

* No External DB connection was used, only a set of fake users in memory in `AuthenticationService` to mimic the authentication process.

* If the user exists in the system we assign him claims such as [**Name**, **Id**, and **Role**], and sign in him with the `Cookies` authentication scheme.

* Exposes `/login` and `/logout` endpoints in `HomeController`, Implements simple role-based access control on some resources in `WeatherForecastController` to validate the functionality of cookie authentication. 

## **JwtAuth**

* Implements JWT authentication in .Net.

* Uses `Microsoft.AspNetCore.Authentication.JwtBearer` library for implementing the JWT authentication process in .Net.

* Exposes `/Signin` and `/Signout` endpoints in `HomeController`.

* Uses MS SQL Server as a database to implement simple user model functionality. Stores hashed passwords.

* Implements the JWT authentication in `AuthenticationService` which signs the current user simple claims such [**Sub**, **Email**, and **Jti**].

* Protects the endpoint in the `WeatherForecastController` to require to be authenticated before accessing the resource.

* Implements `UserDataMiddleware` which is executed immediately after the `AuthenticationMiddleware` to retrieve store user DB model in context by by extracting his Id from claims.  

## **MultipleAuthSchemes**

* Implements 2 Cookie-based authentication schemes, one is dedicated for `regular` users and the other for `special` users

* Exposes `/login-regular`, `/logout-regular` for regular users and `/login-special`, `/logout-special` for special users in `HomeController`

* No External DB connection was used, only a set of fake users in memory in `AuthenticationService` to mimic the authentication process.

* Fake user will can authenticate himself with both endpoints; and he will get the corresponding role `regular` or `special` as well.

* Overrides the `AuthorizationPolicyBuilder` behavior in `PoliciesExtensions` to accept authenticated users from both schemes instead of the default one only.

* Defines `OnlySpecialUsers` authorization policy which only authorizes special users for chosen resources.

* Validates the functionality of muliple authentication schemes in `WeatherForecastController` by defining an accessible resource by both schemes, and by defining a dedicated resource only for special users using the above mentioned policy.  

## **SessionAuth**

* Implements session-based authentication from scratch by introducing new custom session-based authentication scehem and its corresponding logic handler.

* No External DB connection was used, only a set of fake users and roles implemented in-memory in `InMemoryUserService`.

* An in-memory session manager implementation for managing usesr's session resides in `InMemorySessionManager`. Provides several functionalities starting from session **Creation**, **Retrieving**, and **Revoking** sessions.

* Exposes `/sign-in` and `/sign-out` endpoints for users authentication in `HomeController`

* As mentioned previously, this project introduces new authentication scheme and its corresponding handler by:
  * Introducing `SessionAuthenticationOptions` options class to customize the functionality of the implementation. 
  * Introducing `SessionAuthenticationHandler` which handles the following functionalities:
    * Implements `HandleSignInAsync` method which creates a new user session and writes the session identifier to cookies, 
    * Implements `HandleSignOutAsync` method which reads the session from the cookies and revoke it, then it clears the cookies from that session.
    * Implements `HandleAuthenticateAsync` method which returns an **AuthenticationResult** representing the validity of the session. That's done by checking if the session is not revoked nor expired. After that if everything is validated, then we append a **ClaimsPrinciple** to the **AuthenticationResult** instance representing the user's context to be used later for authroization in the application.
  * Registering the custom session-based authentication handler as our default authentication scheme in our application, that's done by using the methods provided by `SessionExtensions` to register our scheme. It's been done in `AuthExtensions` 

* `HomeController` uses `AuthenticationService` internally which in turn specifies our new custom scheme to sign in and sign out users.

* `WeatherForecastController` introduces endpoints and authorizes access to some of them based on the user's roles, which got evaluated and populated in `SessionAuthenticationHandler`. This proves the correctness of our custom scheme implementation.  

## **PBAC**

* Implements permission-based access control, and uses Jwt for authentication

* The following relationship between entities is implemented where:
  * `User` represents the users in the system.
  * `Role` are system roles
  * `Permission` are system permissions
  * `UserRole` and `RolePermission` are junction tables to represent m-m relationships between entities. 
  
![image](https://github.com/user-attachments/assets/d9271e5e-2930-4dfe-814c-5add7df40c24)

* Uses MS SQL Server as a database to implement the mentioned relationship.
 
* Exposes `/login` and `/sign-up` endpoints in `UserController` to authenticate and register users.

* Once the user is authenticated, he will receive a JWT token containing all his permissions for the system. That's managed by `UserService`

* The process of authorizaing users and determining their access level is implemented by exploiting the .Net built-in policy-based authorization and by overriding some of the built-in .Net identity features and introducing some attributes and helpers such:
  * Introducing `PermissionRequirement` class which will hold the permission name dedicated for a specific resource
  * Introducing `PermissionAuthorizationHandler` which handles and determins if the user is authorized to access a resource by extracing his permissions from his Jwt token.
  * Overriding `DefaultAuthorizationPolicyProvider` behavior in `PermissionPolicyProvider` and registering it as the default one in the project. So if our policies start with the special prefix **"CustomPermission:"** then we build a policy at run time and inject to it **PermissionRequirement**
  * Introducing `HasPermissionAttribute` class which handles the abstraction of adding the special prefix to each permission.
  * Now the developers can use the `HasPermission` attribute, and pass to it the permission name the user should have to access the resource.

* The flow for of how the system will work when a user tries to access an endpoint decorated with `HasPermission` attribute:
  * The .Net built-in `AuthorizationService` will extract the permission name from the `HasPermission` which is a policy name
  * The overridden `PermissionPolicyProvider` will detect that this permission starts with special prefix and will build a policy at run time containing `PermissionRequirement` requirement
  * After getting the policy built at run time, the `AuthorizationService` will evaluate this policy by invoking the requirement's authorization handler which is `PermissionAuthorizationHandler`, and it will check if there is a permission in user's jwt matches the one specified at `HasPermission` attribute.

* `ProductController`, `ItemController` and their respective services and entities were introduced as resources so authorized users can manipulate them.

* `PermissionController` is introduced to provide admins the ability to manipulate permissions, roles, and managing other users' permissions dynamically.  

## **OAuth**

* Implements OAuth flow for granting the application access to third-part applications.

* The application does not use any external libraries but the .Net built-in implementation for OAuth.

* The application authenticates users and serves them without storing any kind of users' information in its own system, so no Database was used at all.

* The third-party application in this project was `GitHub`. In this project, we try to access user's repositories from GitHub.

* User secrets feature in .Net was used to store **ClientId** and **ClientSecret** for the GitHub registered application, so if you want to run this project, you need to provide yours.

* The application requests only one scope `repo`, which grant the application access to all the user's repositories.

* These are the following third-party (GitHub) endpoints provided for the OAuth flow:
  * Authorization endpoint: `https://github.com/login/oauth/authorize`
  * Token endpoint: `https://github.com/login/oauth/access_token`
  * User info endpoint: `https://api.github.com/user`

* The application flow of OAuth flow as follows:
  * User starts the OAuth flow by the hitting the exposed `/login/github` endpoint in `HomeController`, so the application can request an access to the user account from GitHub.
  * Upon user consent for the application to access the scopes the application requested, the application gets an authorization code to exchange it for access token later.
  * The application gets an access token after exchanging the authorization code and it stores it in the user's context, specifically in `AuthorizationProperties`.
  * The application uses .Net `Cookies` authentication for creating user context upon successful OAuth flow.

* Don't try to start the flow by hitting `/login/github` using **Swagger** endpoint you will hit error, instead open a tab and specify the full path for the endpoints.   

* Upon successful OAuth flow, the user can use the endpoints provided by `UserController` which internally uses `GithubUserService`:
  * `/github/me` endpoint to get user's GitHub profile information. That's done by hitting a request to the **User info** endpoint mentioned above using the granted access token.
  * `/github/repositories/me` endpoint to list all the user's GitHub repositories; and for sure, that's done through passing the previously granted access token to `https://api.github.com/users/{username}/repos` endpoint.

## **OpenIdConnect**

* Implements OpenIdConnect flow for authenticating/logging users with external providers.

* The external providers the application integrates with are `Google` and `GitHub`.

* User secrets feature in .Net was used to store **ClientId** and **ClientSecret** for both **Google** and **GitHub** registered application, so if you want to run this project, you need to provide yours.

* It's important to mention that `GitHub` does not implement OpenIdConnect protocol, so to get the identity of the user we have to manually hit the **user info** endpoint to get his identity information.

* This POC project does not only implement OIDC but also extends further the scope to try and manage its own data by storing authenticated users in its own DB and assigning users roels which are also managed by the system. 

* `Google` does implement OpenIdConnect protocol, so upon successful exchange for the authroization code, we will get an ID token which will contain the user's identity information; also it provides **user info** endpoint for more details about the user. 

* `Microsoft.AspNetCore.Authentication.OpenIdConnect` was used for integrating with `Google` while built-in .Net OAuth implementation was used for `GitHub`.

* The endpoints provided by external providers:
  * Google: 
    * Authorization endpoint: `https://accounts.google.com/o/oauth2/v2/auth`
    * Token endpoint: `https://oauth2.googleapis.com/token`
    * User info endpoint: `https://openidconnect.googleapis.com/v1/userinfo`
  * GitHuib: 
    * Authorization endpoint: `https://github.com/login/oauth/authorize`
    * Token endpoint: `https://github.com/login/oauth/access_token`
    * User info endpoint: `https://api.github.com/user`

* The following relationship between entities is implemented where:
  * `User` represents the system users.
  * `Role` represents the system roles.
  * `ExternalLogin` represents the login by external providers, e.g **Google**, **GitHub**. The relationship with `User` is 1-m.
  * `ExternalLoginRole` represents a junction table to link each external login with its roles.
   
![image](https://github.com/user-attachments/assets/ce8019f9-b611-4aff-ba13-172d0e1f1b9d)

* Uses MS SQL Server as a database to implement the mentioned relationship.

* The flow of **Google** OIDC as follows:
  * User starts the OIDC flow by hitting the exposed `/login/google` endpoint in `HomeController`, so the application can request an access to the user account from Google.
  * Internally the used library append `openid` scope to the scopes going to be request from Google authorization server.
  * Upon user consent for the application to access the scopes the application requested, the application gets an authorization code to exchange it for access and ID tokens later.
  * The application gets access and Id tokens after exchanging the authorization code, extracts user information and stores them in the user's context, specifically in `AuthorizationProperties`.
  * The system ensures the user created and stored in the its own DB.
  * The system assigns the user's roles and append them to the context.
  * The application uses .Net `Cookies` authentication for creating user context upon successful OIDC flow.

* The flow of **GitHub** OAuth used for authentication as follows:
  * User starts the OAuth flow by the exposes `/login/github` endpoint in `HomeController`, so the application can request an access to the user account from GitHub.
  * Upon user consent for the application to access the scopes the application requested, the application gets an authorization code to exchange it for access token later.
  * The application gets an access token after exchanging the authorization code and it stores it in the user's context, specifically in `AuthorizationProperties`.
  * The application uses the granted access token to hit `https://api.github.com/user` to get user info and extracts properties and stores them in user's context also.
  * The system ensures the user created and stored in the its own DB.
  * The system assigns the user's roles and append them to the context.
  * The application uses .Net `Cookies` authentication for creating user context upon successful OAuth flow.

* Don't try to start the flow by hitting `/login/github` or `/login/google` using **Swagger** endpoint you will hit error, instead open a tab and specify the full path for the endpoints.   

* The custom logic for storing users in the system's DB and managing roles is achieved through exploiting the provided Events in OAuth/OIDC .Net implementations.

*  `ResourceController` exposes endpoints with different access levels depending on the user's roles, this is to ensure and validate the correctness of the implementation.

### Extra Resources:
* [Web Authentication Methods Compared](https://testdriven.io/blog/web-authentication-methods/?fbclid=IwAR1s6B2DtCs5lBtNv91miQIqzxa6Ev60uBWtVkvxYFFyuGOw4MTGH9KWEuw#token-based-authentication)
* [Cookie/Session-based Auth with Nodejs](https://www.sohamkamani.com/nodejs/session-cookie-authentication/)
* [ASP.NET Core Authentication (.NET 7 Minimal APIs)](https://www.youtube.com/watch?v=ExQJljpj1lY&list=PLOeFnOV9YBa4yaz-uIi5T4ZW3QQGHJQXi) 
