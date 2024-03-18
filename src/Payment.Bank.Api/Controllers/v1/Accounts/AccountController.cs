using Ardalis.GuardClauses;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.FeatureManagement.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Payment.Bank.Api.Options;
using Payment.Bank.Api.Extensions;
using Payment.Bank.Application.Accounts.Features.ActivateAccount.v1;
using Payment.Bank.Application.Accounts.Services;
using Payment.Bank.Application.Accounts.Features.GetAccount.v1;
using Payment.Bank.Application.Accounts.Features.CreateAccount.v1;
using Payment.Bank.Application.Accounts.Features.DeactivateAccount.v1;

namespace Payment.Bank.Api.Controllers.v1.Accounts;

[ApiController]
[ApiVersion("1.0", Deprecated = false)]
[Route("/api/v{version:ApiVersion}/[controller]")]
[Produces(Constants.MimeTypes.ApplicationJson)]
[SwaggerTag("Create, Read, Update and Delete Accounts")]
public sealed class AccountController(IOptions<ApiOptions> apiOptions, ILogger<AccountController> logger) : ControllerBase
{
    private readonly ApiOptions _apiOptions = Guard.Against.Null(apiOptions, nameof(apiOptions)).Value;
    private readonly ILogger<AccountController> _logger = Guard.Against.Null(logger, nameof(logger));

    [HttpGet("{accountNumber:int}", Name = "GetByAccountNumber")]
    [FeatureGate(Constants.Features.GetAccount)]
    [SwaggerOperation(
        OperationId = nameof(GetByAccountNumberAsync),
        Description = "Gets an account by account number.",
        Tags = ["Account"])]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GetAccountResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(BadRequest<ProblemDetails>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(NotFound))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<Results<Ok<GetAccountResponse>, BadRequest<ProblemDetails>, NotFound>> GetByAccountNumberAsync(
        [FromServices] IAccountService accountService,
        [Required][FromRoute] int accountNumber,
        CancellationToken cancellationToken = default)
    {
        this._logger.Log(LogLevel.Information, "Getting account by {accountNumber}", accountNumber);

        var query = new GetAccountQuery(accountNumber);

        var result = await accountService
            .GetAccountAsync(query, cancellationToken)
            .ConfigureAwait(false);

        return result.Match<Results<Ok<GetAccountResponse>, BadRequest<ProblemDetails>, NotFound>>(
            getAccountResponse => TypedResults.Ok(getAccountResponse),
            validationResult => validationResult.ToBadRequest(this._apiOptions.DocumentationUrl),
            _ => TypedResults.NotFound());
    }

    [HttpPost("", Name = "CreateAccount")]
    [FeatureGate(Constants.Features.CreateAccount)]
    [SwaggerOperation(
        OperationId = nameof(CreateAccountAsync),
        Description = "Creates an account.",
        Tags = ["Account"])]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CreateAccountResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(BadRequest<ProblemDetails>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<Results<Created<CreateAccountResponse>, BadRequest<ProblemDetails>>> CreateAccountAsync(
        [FromServices] IAccountService accountService,
        [Required][FromBody] CreateAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        this._logger.Log(LogLevel.Information, "Creating account for {command}", command);

        var result = await accountService
            .CreateAccountAsync(command, cancellationToken)
            .ConfigureAwait(false);

        return result.Match<Results<Created<CreateAccountResponse>, BadRequest<ProblemDetails>>>(
            createAccountResponse => TypedResults.Created($"api/v1/account/{createAccountResponse.AccountNumber}", createAccountResponse),
            validationResult => validationResult.ToBadRequest(this._apiOptions.DocumentationUrl));
    }

    [HttpPut("activate/{accountNumber:int}", Name = "ActivateAccount")]
    [FeatureGate(Constants.Features.ActivateAccount)]
    [SwaggerOperation(
        OperationId = nameof(ActivateAccountAsync),
        Description = "Activates an account.",
        Tags = ["Account"])]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ActivateAccountResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(BadRequest<ProblemDetails>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(NotFound))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<Results<Ok<ActivateAccountResponse>, BadRequest<ProblemDetails>, NotFound>> ActivateAccountAsync(
        [FromServices] IAccountService accountService,
        [Required][FromRoute] int accountNumber,
        CancellationToken cancellationToken = default)
    {
        this._logger.Log(LogLevel.Information, "Activating account for {accountNumber}", accountNumber);

        var command = new ActivateAccountCommand(accountNumber);

        var result = await accountService
            .ActivateAccountAsync(command, cancellationToken)
            .ConfigureAwait(false);

        return result.Match<Results<Ok<ActivateAccountResponse>, BadRequest<ProblemDetails>, NotFound>>(
            activateAccountResponse => TypedResults.Ok(activateAccountResponse),
            validationResult => validationResult.ToBadRequest(this._apiOptions.DocumentationUrl),
            _ => TypedResults.NotFound());
    }

    [HttpPut("deactivate/{accountNumber:int}", Name = "DeactivateAccount")]
    [FeatureGate(Constants.Features.DeactivateAccount)]
    [SwaggerOperation(
        OperationId = nameof(DeactivateAccountAsync),
        Description = "Deactivates an account.",
        Tags = ["Account"])]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CreateAccountResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(BadRequest<ProblemDetails>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(NotFound))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<Results<Ok<DeactivateAccountResponse>, BadRequest<ProblemDetails>, NotFound>> DeactivateAccountAsync(
        [FromServices] IAccountService accountService,
        [Required][FromRoute] int accountNumber,
        CancellationToken cancellationToken = default)
    {
        this._logger.Log(LogLevel.Information, "Deactivating account for {accountNumber}", accountNumber);

        var command = new DeactivateAccountCommand(accountNumber);

        var result = await accountService
            .DeactivateAccountAsync(command, cancellationToken)
            .ConfigureAwait(false);

        return result.Match<Results<Ok<DeactivateAccountResponse>, BadRequest<ProblemDetails>, NotFound>>(
            deactivateAccountResponse => TypedResults.Ok(deactivateAccountResponse),
            validationResult => validationResult.ToBadRequest(this._apiOptions.DocumentationUrl),
            _ => TypedResults.NotFound());
    }
}
