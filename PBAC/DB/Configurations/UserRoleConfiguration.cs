using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PBAC.Models.Db;

namespace PBAC.DB.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(x => new { x.UserId, x.RoleId });

        builder.HasData(new List<UserRole>()
            {
                new() { UserId = new Guid("a8315f05-f401-4589-b976-61d129d3b1d7"), RoleId = 1 },    // Admin for this user
                new() { UserId = new Guid("54bfc54e-386b-455f-b627-5b8800d7fc1a"), RoleId = 2 },    // Moderator for this user
                new() { UserId = new Guid("8db76dd5-c7ff-4550-b471-38d3f73446bb"), RoleId = 3 },    // User role for this user
                new() { UserId = new Guid("ffb2061b-815e-4f4a-a981-7846f76954ef"), RoleId = 2 },    // Moderator & user roles for this user
                new() { UserId = new Guid("ffb2061b-815e-4f4a-a981-7846f76954ef"), RoleId = 3 }
            });
    }
}
