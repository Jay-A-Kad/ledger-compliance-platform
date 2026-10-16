
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LedgerPlatform.Application.Transactions;


public record CreateTransfer(Guid SourceAccountId, Guid DestinationAccountId, decimal Amount, Guid ActorUserId,
        string IdempoKey) : IRequest<TransactionResponse>, IRequiresOptimisticRetry, IIdempotentCommand<TransactionResponse>
    {
        public async Task<TransactionResponse?> FindExistingResultAsync(IApplicationDbContext dbContext, CancellationToken cancellationToken)
        {
            var existingTransaction = await dbContext.Transactions
                .FirstOrDefaultAsync(t => t.IdempoKey == IdempoKey, cancellationToken);

            if(existingTransaction is null)
            {
                return null;
            }

            return new TransactionResponse(existingTransaction.Id, 
            existingTransaction.TransactionStatus, 
            existingTransaction.Amount, 
            existingTransaction.CreatedAt);

              
        }
    }