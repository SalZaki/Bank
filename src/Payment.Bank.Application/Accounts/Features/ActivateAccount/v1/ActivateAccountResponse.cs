using System.Diagnostics.CodeAnalysis;

namespace Payment.Bank.Application.Accounts.Features.ActivateAccount.v1;

[ExcludeFromCodeCoverage]
public sealed record ActivateAccountResponse(string AccountStatus);
