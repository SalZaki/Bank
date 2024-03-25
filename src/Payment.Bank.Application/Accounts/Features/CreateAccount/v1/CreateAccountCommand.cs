using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Payment.Bank.Common.Abstractions.Commands;

namespace Payment.Bank.Application.Accounts.Features.CreateAccount.v1;

[ExcludeFromCodeCoverage]
public sealed record CreateAccountCommand : ICommand
{
    [JsonPropertyName("account_id")]
    public Guid? AccountId { get; init; }

    [JsonPropertyName("account_number")]
    public int? AccountNumber { get; init; }

    [JsonPropertyName("account_holder_name")]
    public required string AccountHolderName { get; init; }

    [JsonPropertyName("account_balance")]
    public decimal? AccountBalance { get; init; }

    [JsonPropertyName("account_type")]
    public string? AccountType { get; init; }

    [JsonPropertyName("sort_code")]
    public int? SortCode { get; init; }

    [JsonPropertyName("iban")]
    public string? Iban { get; init; }
}
