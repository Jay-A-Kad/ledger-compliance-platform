using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using LedgerPlatform.Domain.Entities;

namespace LedgerPlatform.Infrastructure.Configurations;




public class AccountBalanceProjectionConfiguration : IEntityTypeConfiguration<AccountBalanceProjection>
{
    public void Configure(EntityTypeBuilder<AccountBalanceProjection> builder)
    {
        //pk
        builder.HasKey(le => le.AccountId);

        builder.Property(le => le.Balance)
            .HasColumnType("decimal(18,2)");


        builder.HasOne<LedgerEvent>()
            .WithMany()
            .HasForeignKey(le => le.LastEventId)
            .OnDelete(DeleteBehavior.Restrict);



        //fk
        builder.HasOne<Account>()
            .WithOne()
            .HasForeignKey<AccountBalanceProjection>(le => le.AccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}