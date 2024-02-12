using Microsoft.EntityFrameworkCore;
using PBAC.Models.Db;

namespace PBAC.DB;

public class PbacContext : DbContext
{
    public PbacContext(DbContextOptions<PbacContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(x => x.Id);
    }
}
