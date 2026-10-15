using MediatR;

namespace LedgerPlatform.Application.Transactions;


public record CreateTransfer(Guid SourceAccountId, Guid DestinationAccountId, decimal Amount, Guid ActorUserId, string IdempoKey) : IRequest<TransactionResponse>;

