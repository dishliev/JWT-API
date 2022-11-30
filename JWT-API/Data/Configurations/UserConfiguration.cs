using JWT_API.Helpers;
using JWT_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JWT_API.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            Guid guid = Guid.NewGuid();

            builder.HasData(new User()
            {
                Id = guid,
                UserName = "testUser",
                Password = PasswordHash.CreateHash("d@rsa!WaDFsdI")
            });
        }
    }
}