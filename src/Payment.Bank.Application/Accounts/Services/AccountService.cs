using System.Collections.ObjectModel;
using Ardalis.GuardClauses;
using FluentValidation.Results;
using OneOf;
using OneOf.Types;
using Payment.Bank.Application.Accounts.Features.ActivateAccount.v1;
using Payment.Bank.Common.Abstractions.Queries;
using Payment.Bank.Common.Abstractions.Commands;
using Payment.Bank.Application.Accounts.Features.CreateAccount.v1;
using Payment.Bank.Application.Accounts.Features.DeactivateAccount.v1;
using Payment.Bank.Application.Accounts.Features.GetAccount.v1;

namespace Payment.Bank.Application.Accounts.Services;

public class AccountService(
    IQueryHandler<GetAccountQuery, OneOf<GetAccountResponse, ReadOnlyCollection<ValidationFailure>, NotFound>> getAccountQueryHandler,
    ICommandHandler<CreateAccountCommand, OneOf<CreateAccountResponse, ReadOnlyCollection<ValidationFailure>>> createAccountCommandHandler,
    ICommandHandler<ActivateAccountCommand, OneOf<ActivateAccountResponse, ReadOnlyCollection<ValidationFailure>, NotFound>> activateAccountCommandHandler,
    ICommandHandler<DeactivateAccountCommand, OneOf<DeactivateAccountResponse, ReadOnlyCollection<ValidationFailure>, NotFound>> deactivateAccountCommandHandler)
    : IAccountService
{
    private readonly IQueryHandler<GetAccountQuery, OneOf<GetAccountResponse, ReadOnlyCollection<ValidationFailure>, NotFound>>
        _getAccountQueryHandler = Guard.Against.Null(getAccountQueryHandler, nameof(getAccountQueryHandler));

    private readonly ICommandHandler<CreateAccountCommand, OneOf<CreateAccountResponse, ReadOnlyCollection<ValidationFailure>>>
        _createAccountCommandHandler = Guard.Against.Null(createAccountCommandHandler, nameof(createAccountCommandHandler));

    private readonly ICommandHandler<ActivateAccountCommand, OneOf<ActivateAccountResponse, ReadOnlyCollection<ValidationFailure>, NotFound>>
        _activateAccountCommandHandler = Guard.Against.Null(activateAccountCommandHandler, nameof(activateAccountCommandHandler));

    private readonly ICommandHandler<DeactivateAccountCommand, OneOf<DeactivateAccountResponse, ReadOnlyCollection<ValidationFailure>, NotFound>>
        _deactivateAccountCommandHandler = Guard.Against.Null(deactivateAccountCommandHandler, nameof(deactivateAccountCommandHandler));

    public async Task<OneOf<GetAccountResponse, ValidationResult, NotFound>> GetAccountAsync(
        GetAccountQuery query,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(query, nameof(query));

        var result = await this._getAccountQueryHandler.HandleAsync(query, cancellationToken).ConfigureAwait(false);

        return result.Match<OneOf<GetAccountResponse, ValidationResult, NotFound>>(
            getAccountResponse =>  getAccountResponse,
            validationFailed => new ValidationResult(validationFailed),
            notFound => notFound);
    }

    public async Task<OneOf<CreateAccountResponse, ValidationResult>> CreateAccountAsync(
        CreateAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(command, nameof(command));

        var result = await this._createAccountCommandHandler.HandleAsync(command, cancellationToken).ConfigureAwait(false);

        return result.Match<OneOf<CreateAccountResponse, ValidationResult>>(
            createAccountResponse =>  createAccountResponse,
            validationFailed => new ValidationResult(validationFailed));
    }

    public async Task<OneOf<ActivateAccountResponse, ValidationResult, NotFound>> ActivateAccountAsync(
        ActivateAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(command, nameof(command));

        var result = await this._activateAccountCommandHandler.HandleAsync(command, cancellationToken).ConfigureAwait(false);

        return result.Match<OneOf<ActivateAccountResponse, ValidationResult, NotFound>>(
            activateAccountResponse => activateAccountResponse,
            validationFailed => new ValidationResult(validationFailed),
            notFound => notFound);
    }

    public async Task<OneOf<DeactivateAccountResponse, ValidationResult, NotFound>> DeactivateAccountAsync(DeactivateAccountCommand command, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(command, nameof(command));

        var result = await this._deactivateAccountCommandHandler.HandleAsync(command, cancellationToken).ConfigureAwait(false);

        return result.Match<OneOf<DeactivateAccountResponse, ValidationResult, NotFound>>(
            deactivateAccountResponse => deactivateAccountResponse,
            validationFailed => new ValidationResult(validationFailed),
            notFound => notFound);
    }
}
