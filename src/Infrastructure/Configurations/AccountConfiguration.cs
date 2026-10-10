using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LedgerPlatform.Domain.Entities;




namespace LedgerPlatform.Infrastructure.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);

       
    builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(t => t.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.Property(a => a.AccountNumber)  
            .IsRequired();

        builder.HasIndex(a => a.AccountNumber)
            .IsUnique();



        builder.Property(a => a.Currency)
            .IsRequired();

        builder.Property(a => a.CreatedAt)
            .IsRequired();
        

        builder.Property(t => t.AccountType)
            .IsRequired();

    }
}