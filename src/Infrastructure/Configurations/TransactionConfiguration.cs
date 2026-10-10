using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LedgerPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;



namespace LedgerPlatform.Infrastructure.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);


        builder.Property(t => t.Amount)
            .HasColumnType("decimal(18,2)");


        builder.Property(t => t.IdempoKey)
            .IsRequired();

         builder.HasIndex(t => t.IdempoKey)
            .IsUnique();

        builder.HasIndex(t => t.ReverseTransactionId)
            .IsUnique()
            .HasFilter("[ReverseTransactionId] IS NOT NULL");



        builder.HasOne<Account>()
            .WithMany()
            .HasForeignKey(t => t.SrcAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Account>()
            .WithMany()
            .HasForeignKey(t => t.DestAccountId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne<Transaction>()
            .WithMany()
            .HasForeignKey(t => t.ReverseTransactionId)
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}

