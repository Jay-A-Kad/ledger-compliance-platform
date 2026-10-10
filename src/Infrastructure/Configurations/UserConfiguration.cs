using Microsoft.EntityFrameworkCore;
using LedgerPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LedgerPlatform.Infrastructure.Configurations;



public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);


        builder.Property(u => u.Name)
            .IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasIndex(u => u.ExternalId)
            .IsUnique();

        builder.Property(t => t.Role)
            .IsRequired();
            
    }
}