using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Payment.Bank.Application.Accounts.Features.CreateAccount.v1;

[ExcludeFromCodeCoverage]
public sealed record CreateAccountResponse
{
    [JsonPropertyName("account_number")] public int AccountNumber { get; init; }
}
