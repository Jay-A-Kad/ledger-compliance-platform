
namespace LedgerPlatform.Application.Accounts;




public record AccountResponse(Guid Id, string AccountNumber, string Currency, DateTime CreatedAt);
