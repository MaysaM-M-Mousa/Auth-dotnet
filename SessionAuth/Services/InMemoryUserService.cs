using SessionAuth.Models;

namespace SessionAuth.Services;

public class InMemoryUserService : IUserService
{
    private List<User> UsersLists = new List<User>()
    {
        new User(){ Id = new Guid("ec4575be-a93c-4dc6-a7ab-d221461b37ea"), Name = "Ahmad", Email = "ahmad.mousa@gmail.com", Password = "123" },
        new User(){ Id = new Guid("5af8d290-a9af-4c35-80e0-ad316f5a1c3f"), Name = "Maysam", Email = "maysam.mousa@gmail.com", Password = "123" },
        new User(){ Id = new Guid("9fde31ed-08a6-4c97-9c92-99299a992673"), Name = "Fadi", Email = "fadi.mousa@gmail.com", Password = "123" }
    };

    public Dictionary<Guid, List<string>> UserRoles = new()
    {
        { new Guid("ec4575be-a93c-4dc6-a7ab-d221461b37ea"), new(){ "HR"} },
        { new Guid("5af8d290-a9af-4c35-80e0-ad316f5a1c3f"), new(){ "Admin", "HR", "User" } },
        { new Guid("9fde31ed-08a6-4c97-9c92-99299a992673"), new(){ "User" } }
    };

    public Task<User?> GetUser(string email)
    {
        var user = UsersLists.FirstOrDefault(u => u.Email == email);

        return Task.FromResult(user);
    }

    public Task<List<string>> GetUserRoles(Guid userId)
    {
        var userRoles = UserRoles.ContainsKey(userId) ? UserRoles[userId] : new();

        return Task.FromResult(userRoles);
    }
}
