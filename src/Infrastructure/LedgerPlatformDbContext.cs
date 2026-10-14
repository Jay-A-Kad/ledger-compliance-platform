using LedgerPlatform.Application;
using LedgerPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LedgerPlatform.Infrastructure;


public class LedgerPlatformDbContext : DbContext, IApplicationDbContext
{
    public LedgerPlatformDbContext(DbContextOptions<LedgerPlatformDbContext> options) : base(options)
    {

    }

    
    public DbSet<Account> Accounts { get; set;}
    public DbSet<User> Users { get; set;}
    public DbSet<LedgerEvent> LedgerEvents{ get; set;}
    public DbSet<Transaction> Transactions { get; set;}
    public DbSet<AccountBalanceProjection> AccountBalanceProjections { get; set;}

    public async Task<long> GetNextAccountNumberAsync(CancellationToken cancellationToken = default)
    {
        return await Database.SqlQuery<long>($"SELECT NEXT VALUE for AccountNumberSequence")
            .FirstAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LedgerPlatformDbContext).Assembly);
        modelBuilder.HasSequence<long>("AccountNumberSequence");
    }
    

    
} 