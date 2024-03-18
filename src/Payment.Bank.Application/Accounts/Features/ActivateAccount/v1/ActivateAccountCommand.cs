using System.Diagnostics.CodeAnalysis;
using Payment.Bank.Common.Abstractions.Commands;

namespace Payment.Bank.Application.Accounts.Features.ActivateAccount.v1;

[ExcludeFromCodeCoverage]
public sealed record ActivateAccountCommand(int AccountNumber) : ICommand;
