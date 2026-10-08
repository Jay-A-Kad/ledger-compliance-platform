namespace LedgerPlatform.Domain.Entities;

public enum AccountType
{
    Customer,
    ClearingShard
};

public class Account
{
    public Guid Id{ get; private set; }
    public Guid? OwnerId { get; private set; }
    public string AccountNumber { get; private set; }
    public string Currency { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public AccountType AccountType { get; private set; } 


    public Account(Guid id, Guid? ownerId, string accountNumber, string currency, DateTime createdAt, AccountType accountType)
    {
        if(string.IsNullOrWhiteSpace(accountNumber))
        {
            throw new ArgumentException("Account number cannot be null or empty.", nameof(accountNumber));
        }
       
       if(accountType == AccountType.Customer && ownerId == null)
        {
            throw new ArgumentException("OwnerId cannot be null for Customer accounts.", nameof(ownerId));
        }

        if(accountType == AccountType.ClearingShard && ownerId != null)
        {
            throw new ArgumentException("OwnerId must be null for ClearingShard accounts.", nameof(ownerId));
        }

        Id = id;
        OwnerId = ownerId;
        AccountNumber = accountNumber;
        Currency = currency;
        CreatedAt = createdAt;
        AccountType = accountType;
    }
  
}