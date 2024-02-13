using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PBAC.Models.Db;

namespace PBAC.DB.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.HasData(new List<RolePermission>()
                {
                    new() { RoleId = 1, PermissionId = 1 },     // admin - full access
                    new() { RoleId = 1, PermissionId = 2 },
                    new() { RoleId = 1, PermissionId = 3 },
                    new() { RoleId = 1, PermissionId = 4 },
                    new() { RoleId = 1, PermissionId = 5 },
                    new() { RoleId = 1, PermissionId = 6 },
                    new() { RoleId = 1, PermissionId = 7 },
                    new() { RoleId = 1, PermissionId = 8 },
                    new() { RoleId = 1, PermissionId = 9 },

                    new() { RoleId = 2, PermissionId = 1 },     // moderator - create & read for (products & items)
                    new() { RoleId = 2, PermissionId = 4 },
                    new() { RoleId = 2, PermissionId = 5 },
                    new() { RoleId = 2, PermissionId = 8 },

                    new() { RoleId = 3, PermissionId = 4 },     // user - read for (products & items) 
                    new() { RoleId = 3, PermissionId = 8 },
                });
    }
}
