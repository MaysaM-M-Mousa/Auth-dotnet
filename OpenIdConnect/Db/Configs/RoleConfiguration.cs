using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenIdConnect.Constants;
using OpenIdConnect.Db.Models;

namespace OpenIdConnect.Db.Configs;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasData(new List<Role>()
        {
            new(){ Id = 1, Name = Roles.Admin },
            new(){ Id = 2, Name = Roles.Manager },
            new(){ Id = 3, Name = Roles.User },
        });
    }
}
