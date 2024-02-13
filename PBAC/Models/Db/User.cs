namespace PBAC.Models.Db;

public class User
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public List<Role> Roles { get; set; } = new();
}
