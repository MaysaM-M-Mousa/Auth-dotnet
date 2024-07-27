# WebSecurity

This project is a series of Proofs of Concept (POCs) that implements comprehensive web security measures using the .NET framework. Starting with basic authentication methods like Cookie based authentication and JWT authentication, then explores multiple authentication schemes in .Net. Applies Permission-based access control. And implements OAuth/OpenIdConnect for authentication and authorization.

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

* The following relationship between entities is implemented
  
![image](https://github.com/user-attachments/assets/7e31edf0-ffbf-4185-aaf6-8d9e35f90280)

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

* `PermissionController` is introduced to provide admins to manipulate permissions and manage other users' permissions dynamically.  

### Extra Resources:
* [Web Authentication Methods Compared](https://testdriven.io/blog/web-authentication-methods/?fbclid=IwAR1s6B2DtCs5lBtNv91miQIqzxa6Ev60uBWtVkvxYFFyuGOw4MTGH9KWEuw#token-based-authentication)
* [Cookie/Session-based Auth with Nodejs](https://www.sohamkamani.com/nodejs/session-cookie-authentication/)
* [ASP.NET Core Authentication (.NET 7 Minimal APIs)](https://www.youtube.com/watch?v=ExQJljpj1lY&list=PLOeFnOV9YBa4yaz-uIi5T4ZW3QQGHJQXi) 
