using Microsoft.EntityFrameworkCore;
using OpenIdConnect.Db.Models;

namespace OpenIdConnect.Db;

public class OidcDb : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<ExternalLogin> ExternalLogins { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<ExternalLoginRole> ExternalLoginRoles { get; set; }

    public OidcDb(DbContextOptions<OidcDb> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OidcDb).Assembly);
    }
}

