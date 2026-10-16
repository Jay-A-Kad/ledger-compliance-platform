
namespace LedgerPlatform.Application;

public interface IIdempotentCommand<TResponse>
{
    Task<TResponse?> FindExistingResultAsync(IApplicationDbContext dbContext, CancellationToken cancellationToken);
}