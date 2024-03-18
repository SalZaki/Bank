using System.Diagnostics.CodeAnalysis;

namespace Payment.Bank.Application.Accounts.Features.CreateAccount.v1;

[ExcludeFromCodeCoverage]
public sealed record CreateAccountResponse(int AccountNumber);
