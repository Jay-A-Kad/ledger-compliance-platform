using LedgerPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LedgerPlatform.Application;

public interface IApplicationDbContext
{
    public DbSet<Account> Accounts { get;}
    public DbSet<User> Users { get; }
    public DbSet<LedgerEvent> LedgerEvents{ get; }
    public DbSet<Transaction> Transactions { get;}
    public DbSet<AccountBalanceProjection> AccountBalanceProjections { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<long> GetNextAccountNumberAsync(CancellationToken cancellationToken = default);
}