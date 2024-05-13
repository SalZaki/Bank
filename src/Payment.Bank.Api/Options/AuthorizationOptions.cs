using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Payment.Bank.Api.Options;

[ExcludeFromCodeCoverage]
public sealed class AuthorizationOptions
{
    [Required(ErrorMessage = "Configuration 'ApiKey' is required.")]
    public required string ApiKey { get; init; }
}
