using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using LedgerPlatform.Domain.Entities;
namespace LedgerPlatform.Infrastructure.Configurations;




public class LedgerEventConfiguration : IEntityTypeConfiguration<LedgerEvent>
{
    public void Configure(EntityTypeBuilder<LedgerEvent> builder)
    {
        builder.HasKey(le => le.Id);


        builder.Property(le => le.EventType)
            .IsRequired();

        builder.Property(t => t.Amount)
            .HasColumnType("decimal(18,2)");



        builder.HasIndex(le => new {le.AccountId, le.SequenceNumber})
            .IsUnique();
     
        builder.HasOne<Account>()
            .WithMany()
            .HasForeignKey(le => le.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Transaction>()
            .WithMany()
            .HasForeignKey(le => le.TransactionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(le => le.ActorUserId)
            .OnDelete(DeleteBehavior.Restrict);

       
        
    }
}