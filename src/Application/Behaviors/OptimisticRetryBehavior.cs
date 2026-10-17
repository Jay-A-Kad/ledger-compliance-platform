
using System.Diagnostics;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace LedgerPlatform.Application.Behaviors;



public class OptimisticRetryBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IRequiresOptimisticRetry
{
    private readonly IApplicationDbContext _dbContext;

    public OptimisticRetryBehavior(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        for(int attemptCounter = 0; attemptCounter < 3; attemptCounter++)
        {
            try
            {
                return await next();
            }
            catch(DbUpdateException ex) when(ex.InnerException is SqlException sqlEx)
            {
               
                if(sqlEx.Message.Contains("IX_LedgerEvents_AccountId_SequenceNumber"))
                {   
                    if(attemptCounter < 2)
                    {
                        continue;
                    }
                    else
                    {
                        throw new InvalidOperationException("Exhausted Retry. Please try Again Later.", ex);
                    }
                }
                
                if (sqlEx.Message.Contains("IX_Transactions_IdempoKey"))
                {
                    if (request is IIdempotentCommand<TResponse> idempotent)
                    {
                        var existingResult = await idempotent.FindExistingResultAsync(_dbContext, cancellationToken);
                        if(existingResult is not null)
                        {
                            return existingResult;
                        }
                         
                    }
                   
                throw new InvalidOperationException("Idempotency key conflict could not be resolved.", ex);
                }
               
            throw ;
            }
            
            
            
        }
        throw new InvalidOperationException("Retry loop exited without a result.");

    }
}

