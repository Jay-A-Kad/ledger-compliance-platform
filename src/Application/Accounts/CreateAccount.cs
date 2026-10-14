
using MediatR;
namespace LedgerPlatform.Application.Accounts;



public record CreateAccount(Guid OwnerId, string Currency) :IRequest<AccountResponse>;


