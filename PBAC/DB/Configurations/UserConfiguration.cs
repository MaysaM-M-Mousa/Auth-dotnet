using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PBAC.Models.Db;

namespace PBAC.DB.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasData(new List<User>()
            {
                new(){ Id = new Guid("a8315f05-f401-4589-b976-61d129d3b1d7"), Email = "admin@gmail.com", Password = "easyPass", UserName = "Maysam Mousa" },
                new(){ Id = new Guid("54bfc54e-386b-455f-b627-5b8800d7fc1a"), Email = "moderator@gmail.com", Password = "easyPass", UserName = "Some random person" },
                new(){ Id = new Guid("8db76dd5-c7ff-4550-b471-38d3f73446bb"), Email = "user@gmail.com", Password = "easyPass", UserName = "regular user" },
                new(){ Id = new Guid("FFB2061B-815E-4F4A-A981-7846F76954EF"), Email = "mix@gmail.com", Password = "easyPass", UserName = "mix user" }
            });
    }
}
