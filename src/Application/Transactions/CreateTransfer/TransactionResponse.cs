using LedgerPlatform.Domain.Entities;
namespace LedgerPlatform.Application.Transactions;


public record TransactionResponse(Guid TransactionId, TransactionStatus Status, decimal Amount, DateTime CreatedAt);