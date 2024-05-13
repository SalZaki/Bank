using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Payment.Bank.Api.Options;

[ExcludeFromCodeCoverage]
public sealed class CorsOptions
{
    [Required(ErrorMessage = "Configuration 'Name' is required.")]
    [MaxLength(20)]
    public required string Name { get; init; }

    public string[]? Clients { get; init; }

    public bool Enabled { get; init; }
}
