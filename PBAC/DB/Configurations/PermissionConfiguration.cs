using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PBAC.Models.Db;

namespace PBAC.DB.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasData(new List<Permission>()
            {
                new Permission() { Id = 1, Name = "product:create" },
                new Permission() { Id = 2, Name = "product:update" },
                new Permission() { Id = 3, Name = "product:delete" },
                new Permission() { Id = 4, Name = "product:read" },
                new Permission() { Id = 5, Name = "item:create" },
                new Permission() { Id = 6, Name = "item:update" },
                new Permission() { Id = 7, Name = "item:delete" },
                new Permission() { Id = 8, Name = "item:read" },
                new Permission() { Id = 9, Name = "user.permissions:write" },
            });
    }
}
