using Microsoft.EntityFrameworkCore;

namespace JwtAuth.Db;

public class JwtAuthDb : DbContext
{
    public DbSet<User> Users { get; set; }

    public JwtAuthDb(DbContextOptions<JwtAuthDb> options) : base(options){ }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(x => x.Id);
    }
}
