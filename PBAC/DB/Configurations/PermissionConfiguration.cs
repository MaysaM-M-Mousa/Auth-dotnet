using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PBAC.Constants;
using PBAC.Models.Db;

namespace PBAC.DB.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasData(new List<Permission>()
            {
                new Permission() { Id = 1, Name = Permissions.Products.Create },
                new Permission() { Id = 2, Name = Permissions.Products.Update },
                new Permission() { Id = 3, Name = Permissions.Products.Delete },
                new Permission() { Id = 4, Name = Permissions.Products.Read },
                new Permission() { Id = 5, Name = Permissions.Items.Create },
                new Permission() { Id = 6, Name = Permissions.Items.Update },
                new Permission() { Id = 7, Name = Permissions.Items.Delete },
                new Permission() { Id = 8, Name = Permissions.Items.Read },
                new Permission() { Id = 9, Name = Permissions.Users.Write },
            });
    }
}
