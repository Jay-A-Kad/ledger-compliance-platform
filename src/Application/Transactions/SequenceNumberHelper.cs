 using Microsoft.EntityFrameworkCore;
namespace LedgerPlatform.Application.Transactions;

public static class SequenceNumberHelper
{
    
    public static async Task<long> GetNextSequenceNumberAsync(IApplicationDbContext dbContext , Guid accountId, CancellationToken cancellationToken)
    {
        var maxSeq = await dbContext.LedgerEvents
            .Where(e => e.AccountId ==accountId)
            .Select(e => (long?)e.SequenceNumber)
            .MaxAsync(cancellationToken);

            return (maxSeq ?? 0) + 1;
    }  
}