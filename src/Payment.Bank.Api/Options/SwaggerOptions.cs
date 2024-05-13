using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;

namespace Payment.Bank.Api.Options;

[ExcludeFromCodeCoverage]
public sealed class SwaggerOptions
{
    public string? Name { get; init; }

    public required OpenApiInfo Info { get; init; }
    public required string RoutePrefix { get; init; }

    public required string RouteTemplate { get; init; }

    public required string EndpointPath { get; init; }

    public string? BasePath { get; init; }

    public bool Enabled { get; init; }

    public string RoutePrefixWithSlash =>
        string.IsNullOrWhiteSpace(this.RoutePrefix)
            ? string.Empty
            : this.RoutePrefix + "/";
}
