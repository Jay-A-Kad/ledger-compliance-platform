

namespace LedgerPlatform.Domain.Entities;


public enum TransactionStatus
{
    Completed,
    Rejected
}

public class Transaction
{
    public Guid Id{ get; private set; }
    public Guid SrcAccountId { get; private set; }
    public Guid DestAccountId { get; private set; }
    public decimal Amount { get; private set; }
    public TransactionStatus TransactionStatus { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public string IdempoKey {get; private set;}
    public Guid? ReverseTransactionId {get; private set;}

    public Transaction(
        Guid id, 
        Guid srcAccountId, 
        Guid destAccountId, 
        decimal amount, 
        TransactionStatus transactionStatus,
        DateTime createdAt, 
        string idempoKey, 
        Guid? reverseTransactionId = null)
    {
        if(srcAccountId == destAccountId)
        {
            throw new ArgumentException("Source and destination accounts cannot be the same.", nameof(destAccountId));
        }

        if(amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
        }

        if (string.IsNullOrWhiteSpace(idempoKey))
        {
            throw new ArgumentException("IdempotencyKey cannot be null or empty.", nameof(idempoKey));   
        }

        Id = id;
        SrcAccountId = srcAccountId;
        DestAccountId = destAccountId;
        Amount = amount;
        TransactionStatus = transactionStatus;
        CreatedAt = createdAt;
        IdempoKey = idempoKey;
        ReverseTransactionId = reverseTransactionId;

              



    }
}