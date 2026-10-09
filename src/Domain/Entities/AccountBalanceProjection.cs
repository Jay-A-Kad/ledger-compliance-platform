//AccountId, Balance, LastEventId — a read-optimized cache, rebuildable from LedgerEvent at any time


namespace LedgerPlatform.Domain.Entities;

public class AccountBalanceProjection
{
    public Guid AccountId { get; private set; }
    public decimal Balance { get; private set; }
    public Guid LastEventId { get; private set; }


    public AccountBalanceProjection(
        Guid accountId,
        decimal balance, 
        Guid lastEventId)
    {
        AccountId = accountId;
        Balance = balance;
        LastEventId = lastEventId;
    }

    public void UpdateBalance(decimal delta, Guid newEventId)
    {
        Balance += delta;
        LastEventId = newEventId;
    }
}