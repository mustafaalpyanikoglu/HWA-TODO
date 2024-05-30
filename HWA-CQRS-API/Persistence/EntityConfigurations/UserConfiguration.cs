using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        
        builder.Property(u => u.PasswordHash).HasColumnName("PasswordHash").HasColumnType("varbinary(500)").IsRequired();
        builder.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt").HasColumnType("varbinary(500)").IsRequired();
        builder.HasMany(x => x.Assignments).WithOne(x => x.User);
    }
}