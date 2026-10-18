using LedgerPlatform.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LedgerPlatform.Application.Transactions;

public class CreateTransferHandler : IRequestHandler<CreateTransfer, TransactionResponse>
{
      private readonly IApplicationDbContext _dbContext;

      public CreateTransferHandler(IApplicationDbContext dbContext)
      {
          _dbContext = dbContext;
      }

    public async Task<TransactionResponse> Handle(CreateTransfer request, CancellationToken cancellationToken)
    {
        var sourceProjection = await _dbContext.AccountBalanceProjections
            .FirstOrDefaultAsync( p => p.AccountId == request.SourceAccountId, cancellationToken);

        
        var currentBalance = sourceProjection?.Balance ?? 0m;


        //if current balance is lesser than requested amount


        if(currentBalance < request.Amount)
        {
            var rejected = new Transaction(
                Guid.NewGuid(),
                request.SourceAccountId,
                request.DestinationAccountId,
                request.Amount,
                TransactionStatus.Rejected,
                DateTime.UtcNow,
                request.IdempoKey
            );



            _dbContext.Transactions.Add(rejected);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new TransactionResponse(rejected.Id, rejected.TransactionStatus, 
                rejected.Amount, rejected.CreatedAt);
        }


        //if it passes then computing sequence number for both
        var sourceNextSeq = await SequenceNumberHelper.GetNextSequenceNumberAsync(_dbContext, request.SourceAccountId, cancellationToken);
        var destNextSeq = await SequenceNumberHelper.GetNextSequenceNumberAsync(_dbContext, request.DestinationAccountId, cancellationToken);
    

        var transaction = new Transaction(
            Guid.NewGuid(),
            request.SourceAccountId,
            request.DestinationAccountId,
            request.Amount,
            TransactionStatus.Completed,
            DateTime.UtcNow,
            request.IdempoKey
        );

        _dbContext.Transactions.Add(transaction);



        //debit event

        var debitEvent = new LedgerEvent(
            Guid.NewGuid(),
            request.SourceAccountId,
            transaction.Id,
            EventType.Debited,
            sourceNextSeq,
            request.Amount,
            DateTime.UtcNow,
            request.ActorUserId
        );

        //credit event
         var creditEvent = new LedgerEvent(
            Guid.NewGuid(),
            request.DestinationAccountId,
            transaction.Id,
            EventType.Credited,
            destNextSeq,
            request.Amount,
            DateTime.UtcNow,
            request.ActorUserId
        );  

        _dbContext.LedgerEvents.Add(debitEvent);
        _dbContext.LedgerEvents.Add(creditEvent);



        //uodating account balance projection for both acounts

        if(sourceProjection is null)
        {
            _dbContext.AccountBalanceProjections.Add(
                new AccountBalanceProjection(request.SourceAccountId, -request.Amount, debitEvent.Id)
            );
        }
        else
        {
            sourceProjection.UpdateBalance(-request.Amount, debitEvent.Id);
        }

        var destinationProjection = await _dbContext.AccountBalanceProjections
            .FirstOrDefaultAsync(p => p.AccountId == request.DestinationAccountId, cancellationToken);

        if(destinationProjection is null)
        {
            _dbContext.AccountBalanceProjections.Add(
                new AccountBalanceProjection( request.DestinationAccountId, request.Amount,creditEvent.Id)
            );

        }
        else
        {
            destinationProjection.UpdateBalance(request.Amount, creditEvent.Id);
        }


        await _dbContext.SaveChangesAsync(cancellationToken);


        return new TransactionResponse(transaction.Id, transaction.TransactionStatus, transaction.Amount,   transaction.CreatedAt);


    }



}