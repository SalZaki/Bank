using System.Diagnostics.CodeAnalysis;
using Payment.Bank.Common.Abstractions.Commands;

namespace Payment.Bank.Application.Accounts.Features.DeactivateAccount.v1;

[ExcludeFromCodeCoverage]
public sealed record DeactivateAccountCommand(int AccountNumber) : ICommand;
