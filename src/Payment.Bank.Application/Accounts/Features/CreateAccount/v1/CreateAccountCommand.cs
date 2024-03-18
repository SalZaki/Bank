using System.Diagnostics.CodeAnalysis;
using Payment.Bank.Common.Abstractions.Commands;

namespace Payment.Bank.Application.Accounts.Features.CreateAccount.v1;

[ExcludeFromCodeCoverage]
public sealed record CreateAccountCommand : ICommand
{
    public Guid? AccountId { get; init; }

    public int? AccountNumber { get; init; }

    public required string AccountHolderName { get; init; }

    public decimal? AccountBalance { get; init; }

    public string? AccountType { get; init; }

    public int? SortCode { get; init; }

    public string? Iban { get; init; }
}
