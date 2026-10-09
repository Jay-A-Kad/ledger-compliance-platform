
namespace LedgerPlatform.Domain.Entities;

public enum EventType
{
    Credited,
    Debited
}


public class LedgerEvent
{
    public Guid Id { get; private set; }
    public Guid AccountId { get; private set;}
    public Guid TransactionId { get; private set;}
    public EventType EventType { get; private set; }
    public long SequenceNumber { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime OccurredAt { get; private set; }
    public Guid ActorUserId { get; private set; }



    public LedgerEvent(
        Guid id, 
        Guid accountId, 
        Guid transactionId,
        EventType eventType,
        long sequenceNumber,
        decimal amount,
        DateTime occurredAt,
        Guid actorUserId)
    {
        if(amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than zero", nameof(amount));
        }

        if(sequenceNumber <= 0)
        {
            throw new ArgumentException("SequenceNumber must be greater than zero", nameof(sequenceNumber));

        }

        Id = id;
        AccountId = accountId ;
        TransactionId = transactionId;
        EventType = eventType;
        SequenceNumber = sequenceNumber;
        Amount = amount;
        OccurredAt = occurredAt;
        ActorUserId = actorUserId;


    }

}