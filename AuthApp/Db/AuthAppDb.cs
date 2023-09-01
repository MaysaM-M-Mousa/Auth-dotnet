using Microsoft.EntityFrameworkCore;

namespace AuthApp.Db;

public class AuthAppDb : DbContext
{
    public DbSet<User> Users { get; set; }

    public AuthAppDb(DbContextOptions<AuthAppDb> options) : base(options){ }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(x => x.Id);
    }
}
