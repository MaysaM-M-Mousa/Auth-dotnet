using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PBAC.Models.Db;

namespace PBAC.DB.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Permissions)
            .WithMany(x => x.Roles)
            .UsingEntity<RolePermission>();

        builder.HasMany(x => x.Users)
            .WithMany(x => x.Roles)
            .UsingEntity<UserRole>();

        builder.HasData(new List<Role>()
            {
                new Role() { Id = 1, Name = "Admin" },
                new Role() { Id = 2, Name = "Moderator" },
                new Role() { Id = 3, Name = "User" },
            });
    }
}
