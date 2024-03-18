using System.Diagnostics.CodeAnalysis;

namespace Payment.Bank.Application.Accounts.Features.GetAccount.v1;

[ExcludeFromCodeCoverage]
public sealed record GetAccountResponse
{
    public required string AccountId { get; init; }

    public required int AccountNumber { get; init; }

    public required string AccountHolderName { get; init; }

    public required decimal AccountBalance { get; init; }

    public required string AccountType { get; init; }

    public required int SortCode { get; init; }

    public required string Iban { get; init; }

    public required string AccountStatus { get; init; }

    public string? CreatedBy { get; init; }

    public DateTime? CreatedOn { get; init; }
}
