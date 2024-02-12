using Microsoft.EntityFrameworkCore;
using PBAC.Models;

namespace PBAC.DB;

public class PbacContext : DbContext
{
    public PbacContext(DbContextOptions<PbacContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(x => x.Id);
    }
}
