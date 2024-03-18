using System.Diagnostics.CodeAnalysis;

namespace Payment.Bank.Application.Accounts.Features.DeactivateAccount.v1;

[ExcludeFromCodeCoverage]
public sealed record DeactivateAccountResponse(string AccountStatus);
