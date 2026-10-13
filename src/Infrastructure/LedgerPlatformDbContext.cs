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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LedgerPlatformDbContext).Assembly);
    }
    

    
}