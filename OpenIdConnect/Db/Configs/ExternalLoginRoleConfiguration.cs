using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenIdConnect.Db.Models;

namespace OpenIdConnect.Db.Configs;

public class ExternalLoginRoleConfiguration : IEntityTypeConfiguration<ExternalLoginRole>
{
    public void Configure(EntityTypeBuilder<ExternalLoginRole> builder)
    {
        builder.HasKey(x => new { x.ExternalLoginId, x.RoleId });
    }
}
