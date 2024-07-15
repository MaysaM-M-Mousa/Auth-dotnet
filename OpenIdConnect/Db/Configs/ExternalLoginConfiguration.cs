using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenIdConnect.Db.Models;

namespace OpenIdConnect.Db.Configs;

public class ExternalLoginConfiguration : IEntityTypeConfiguration<ExternalLogin>
{
    public void Configure(EntityTypeBuilder<ExternalLogin> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(c => c.UserId);

        builder
            .HasIndex(x => new { x.UserId, x.Provider })
            .IsUnique();
    }
}
