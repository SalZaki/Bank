using System.Text.RegularExpressions;
using Ardalis.GuardClauses;
using Bogus;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Payment.Bank.Api.Options;
using Payment.Bank.Api.Swagger;
using Payment.Bank.Application.Accounts.Features.ActivateAccount.v1;
using Payment.Bank.Application.Accounts.Features.CreateAccount.v1;
using Payment.Bank.Application.Accounts.Features.DeactivateAccount.v1;
using Payment.Bank.Application.Accounts.Features.GetAccount.v1;
using Spectre.Console;

namespace Payment.Bank.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCorsOptions(this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));

        services
            .AddOptions<CorsOptions>()
            .BindConfiguration(Constants.Configuration.CorsSectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    public static void AddApiOptions(this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));

        services
            .AddOptions<ApiOptions>()
            .BindConfiguration(configSectionPath: Constants.Configuration.ApiSectionName,
                configureBinder: options => { options.BindNonPublicProperties = true; })
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    public static void AddSwaggerOptions(this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));

        services
            .AddOptions<SwaggerOptions>()
            .BindConfiguration(configSectionPath: Constants.Configuration.SwaggerSectionName,
                configureBinder: options => { options.BindNonPublicProperties = true; })
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    public static void AddBanner(this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));

        var serviceProvider = services.BuildServiceProvider();
        var apiOptions = serviceProvider.GetRequiredService<IOptions<ApiOptions>>().Value;

        if (apiOptions.BannerEnabled is false)
        {
            return;
        }

        AnsiConsole.Write(new FigletText(apiOptions.Name)
            .Centered().Color(Color.FromInt32(
                new Faker().Random.Number(1, 255))
            )
        );
    }

    public static void AddFeatureManagementFlags(this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));

        var serviceProvider = services.BuildServiceProvider();
        var config = serviceProvider.GetRequiredService<IConfiguration>();

        services.AddFeatureManagement(config);
    }

    public static void AddCorsPolicy(this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));

        var serviceProvider = services.BuildServiceProvider();
        var corsOptions = serviceProvider.GetRequiredService<IOptions<CorsOptions>>().Value;

        if (corsOptions.Enabled is false)
        {
            return;
        }

        if (corsOptions.Clients?.Length > 0)
        {
            services.AddCors(options =>
                options.AddPolicy(corsOptions.Name,
                    builder => builder
                        .WithOrigins(corsOptions.Clients)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()));
        }
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));

        var serviceProvider = services.BuildServiceProvider();
        var swaggerOptions = serviceProvider.GetRequiredService<IOptions<SwaggerOptions>>().Value;

        if (swaggerOptions.Enabled is false)
        {
            return;
        }

        var provider = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

        services.AddSwaggerGen(c =>
        {
            c.DocumentFilter<FeatureGateDocumentFilter>();
            c.EnableAnnotations();

            foreach (var description in provider?.ApiVersionDescriptions!)
            {
                c.SwaggerDoc(description.GroupName, swaggerOptions.Info);
            }
        });
    }

    public static void AddValidators(this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));

        services.AddTransient<IValidator<GetAccountQuery>, GetAccountQueryValidator>();
        services.AddTransient<IValidator<CreateAccountCommand>, CreateAccountCommandValidator>();
        services.AddTransient<IValidator<ActivateAccountCommand>, ActivateAccountCommandValidator>();
        services.AddTransient<IValidator<DeactivateAccountCommand>, DeactivateAccountCommandValidator>();
    }

    public static void AddApiVersion(this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));

        var serviceProvider = services.BuildServiceProvider();
        var apiOptions = serviceProvider.GetRequiredService<IOptions<ApiOptions>>().Value;

        services.AddRouting(o => o.LowercaseUrls = true);
        services.AddVersionedApiExplorer(o =>
        {
            o.GroupNameFormat = "'v'VVV";
            o.SubstituteApiVersionInUrl = true;
        });
        services.AddApiVersioning(o =>
        {
            o.ReportApiVersions = true;
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.DefaultApiVersion = ParseApiVersion(apiOptions.Version);
        });
    }

    private static ApiVersion ParseApiVersion(string? apiVersion)
    {
        if (string.IsNullOrEmpty(apiVersion))
        {
            throw new Exception("ApiVersion version is null or empty.");
        }

        const string VersionPattern = "(.)|(-)";

        var results = Regex
            .Split(apiVersion, VersionPattern)
            .Where(x => x != string.Empty && x != "." && x != "-")
            .ToArray();

        if (results == null || results.Length < 2)
        {
            throw new Exception("Could not parse api version.");
        }

        return results.Length > 2
            ? new ApiVersion(Convert.ToInt32(results[0]), Convert.ToInt32(results[1]), results[2])
            : new ApiVersion(Convert.ToInt32(results[0]), Convert.ToInt32(results[1]));
    }
}
