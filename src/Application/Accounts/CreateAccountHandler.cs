using LedgerPlatform.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LedgerPlatform.Application.Accounts;


public class CreateAccountHandler : IRequestHandler<CreateAccount, AccountResponse>
{

    private readonly IApplicationDbContext _dbContext;

    public CreateAccountHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AccountResponse> Handle(CreateAccount request, CancellationToken cancellationToken)
    {
        var nextNumber = await _dbContext.GetNextAccountNumberAsync(cancellationToken);

        var accountNumber = nextNumber.ToString("D10");

        var account = new Account(
            Guid.NewGuid(),
            request.OwnerId,
            accountNumber,
            request.Currency,
            DateTime.UtcNow,
            AccountType.Customer
        );


        _dbContext.Accounts.Add(account);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AccountResponse(account.Id, account.AccountNumber, account.Currency, account.CreatedAt);
    }
}
