using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenIdConnect.Db.Models;

namespace OpenIdConnect.Db.Configs;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasData(new List<Role>()
        {
            new(){ Id = 1, Name = "Admin" },
            new(){ Id = 2, Name = "Manager" },
            new(){ Id = 3, Name = "User" },
        });
    }
}
