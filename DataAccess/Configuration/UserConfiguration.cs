using Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.UserId);
            builder.Property(c => c.UserName);
            builder.Property(c => c.CPF);
            builder.Property(c => c.PasswordHash);
        }
    }
}
