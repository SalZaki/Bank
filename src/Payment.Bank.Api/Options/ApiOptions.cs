using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Payment.Bank.Api.Options;

[ExcludeFromCodeCoverage]
public sealed class ApiOptions
{
    [Required(ErrorMessage = "Configuration 'Name' is required.")]
    [MaxLength(20)]
    public required string Name { get; init; }

    [Required(ErrorMessage = "Configuration 'BaseUrl' is required.")]
    [MaxLength(20)]
    public required string BaseUrl { get; init; }

    [Required(ErrorMessage = "Configuration 'Version' is required.")]
    [MaxLength(10)]
    public required string Version { get; init; }

    [Required(ErrorMessage = "Configuration 'Authorization' is required.")]
    public required AuthorizationOptions Authorization { get; init; }

    public string? DocumentationUrl { get; init; }

    public bool ReportApiVersions { get; init; }

    public bool BannerEnabled { get; init; }

    public bool LatencyRequestLoggingEnabled { get; init; }

    public string? ApiVersionHeader { get; init; }
}
