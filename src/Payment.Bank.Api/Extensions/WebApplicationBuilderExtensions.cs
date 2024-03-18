using System.Collections.ObjectModel;
using FluentValidation.Results;
using OneOf;
using OneOf.Types;
using Payment.Bank.Application.Accounts.Features.ActivateAccount.v1;
using Payment.Bank.Application.Accounts.Features.CreateAccount.v1;
using Payment.Bank.Application.Accounts.Features.DeactivateAccount.v1;
using Payment.Bank.Application.Accounts.Features.GetAccount.v1;
using Payment.Bank.Application.Accounts.Repositories;
using Payment.Bank.Application.Accounts.Services;
using Payment.Bank.Common.Abstractions.Commands;
using Payment.Bank.Common.Abstractions.Queries;
using Payment.Bank.Common.Mappers;
using Payment.Bank.Common.Utilities;
using Payment.Bank.Domain.Entities;
using Payment.Bank.Infrastructure.Repositories;
using Serilog;

namespace Payment.Bank.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddCoreOptions(this WebApplicationBuilder builder)
    {
        // Cors options
        builder.Services.AddCorsOptions();

        // Api options
        builder.Services.AddApiOptions();

        // Swagger options
        builder.Services.AddSwaggerOptions();
    }

    public static void AddConfigurations(this WebApplicationBuilder builder)
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.local.json", optional: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
            .AddEnvironmentVariables(prefix: "BANK_");
    }

    public static void AddCoreServices(this WebApplicationBuilder builder)
    {
        // Banner
        builder.Services.AddBanner();

        // Cors Policy
        builder.Services.AddCorsPolicy();

        // Api Version
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddApiVersion();

        // Swagger
        builder.Services.AddSwagger();

        // Validators
        builder.Services.AddValidators();

        // Controllers
        builder.Services.AddControllers();

        // Feature management flags
        builder.Services.AddFeatureManagementFlags();

        // Repositories
        builder.Services.AddSingleton<IAccountRepository, InMemoryAccountRepository>();

        // Services
        builder.Services.AddTransient<IAccountService, AccountService>();

        // Mappers
        builder.Services.AddTransient<IMapper<Account, GetAccountResponse>, AccountToGetAccountResponseMapper>();

        // Query Handlers
        builder.Services.AddTransient<IQueryHandler<GetAccountQuery, OneOf<GetAccountResponse, ReadOnlyCollection<ValidationFailure>, NotFound>>, GetAccountHandler>();

        // Command Handlers
        builder.Services.AddTransient<ICommandHandler<CreateAccountCommand, OneOf<CreateAccountResponse, ReadOnlyCollection<ValidationFailure>>>, CreateAccountHandler>();
        builder.Services.AddTransient<ICommandHandler<ActivateAccountCommand, OneOf<ActivateAccountResponse, ReadOnlyCollection<ValidationFailure>, NotFound>>, ActivateAccountHandler>();
        builder.Services.AddTransient<ICommandHandler<DeactivateAccountCommand, OneOf<DeactivateAccountResponse, ReadOnlyCollection<ValidationFailure>, NotFound>>, DeactivateAccountHandler>();
    }

    public static void AddConfiguredSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.WithProperty("Application", builder.Environment.ApplicationName)
                .Enrich.WithProperty("Version", Assembly.GetAssemblyVersion<Program>())
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName();
        });
    }
}
