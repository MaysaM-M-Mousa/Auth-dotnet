namespace PBAC.Models.Db;

public class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public List<User> Users { get; set; } = new();

    public List<Permission> Permissions { get; set; } = new();
}
