using Microsoft.EntityFrameworkCore;
using PBAC.Models.Db;

namespace PBAC.DB;

public class PbacContext : DbContext
{
    public PbacContext(DbContextOptions<PbacContext> options) : base(options) { }

    // PBAC
    public DbSet<User> Users { get; set; } = null!;
    
    public DbSet<Role> Roles { get; set; } = null!;
    
    public DbSet<Permission> Permissions { get; set; } = null!;

    public DbSet<RolePermission> RolesPermissions { get; set; } = null!;

    public DbSet<UserRole> UsersRoles { get; set; } = null!;

    // business models
    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<Item> Items { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PbacContext).Assembly);
    }
}
