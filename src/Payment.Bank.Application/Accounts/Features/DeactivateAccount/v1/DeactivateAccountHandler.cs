using System.Collections.ObjectModel;
using Ardalis.GuardClauses;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using Payment.Bank.Application.Accounts.Repositories;
using Payment.Bank.Common.Abstractions.Commands;
using Payment.Bank.Common.Exceptions;
using Payment.Bank.Domain.Entities;
using Payment.Bank.Domain.ValueObjects;

namespace Payment.Bank.Application.Accounts.Features.DeactivateAccount.v1;

public class DeactivateAccountHandler(
    IAccountRepository accountRepository,
    FluentValidation.IValidator<DeactivateAccountCommand> validator,
    ILogger<DeactivateAccountHandler> logger)
    : ICommandHandler<DeactivateAccountCommand, OneOf<DeactivateAccountResponse, ReadOnlyCollection<ValidationFailure>, NotFound>>
{
    private readonly IAccountRepository _accountRepository =
        Guard.Against.Null(accountRepository, nameof(accountRepository));

    private readonly FluentValidation.IValidator<DeactivateAccountCommand> _validator =
        Guard.Against.Null(validator, nameof(validator));

    private readonly ILogger _logger = Guard.Against.Null(logger, nameof(logger));

    public async Task<OneOf<DeactivateAccountResponse, ReadOnlyCollection<ValidationFailure>, NotFound>> HandleAsync(
        DeactivateAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(command, nameof(command));

        var validationResult = await this._validator.ValidateAsync(command, cancellationToken).ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return validationResult.Errors.AsReadOnly();
        }

        var accountNumber = AccountNumber.Create(command.AccountNumber);

        var result = await this.DeactivateAccountInternal(accountNumber, cancellationToken);

        return result.Match<OneOf<DeactivateAccountResponse, ReadOnlyCollection<ValidationFailure>, NotFound>>(
            activateAccountResponse => activateAccountResponse,
            notFound => notFound);
    }

    private async Task<OneOf<DeactivateAccountResponse, NotFound>> DeactivateAccountInternal(
        AccountNumber accountNumber,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var accountResult = await this.GetAccountAsync(accountNumber, cancellationToken);

            if(accountResult.TryPickT1(out var notFound, out var account))
            {
                return notFound;
            }

            account.Deactivate();

            await this._accountRepository.UpdateAsync(account, cancellationToken);

            return new DeactivateAccountResponse(account.AccountStatus);
        }
        catch (CustomException ex)
        {
            this._logger.Log(LogLevel.Error, "{Message}, {ErrorMessages}, {StatusCode}", ex.Message, ex.ErrorMessages, ex.StatusCode);

            throw;
        }
    }

    private async Task<OneOf<Account, NotFound>> GetAccountAsync(
        AccountNumber accountNumber,
        CancellationToken cancellationToken = default)
    {
        var account = await this._accountRepository.FindOneAsync(x => x.AccountNumber == accountNumber, cancellationToken);

        return account == Account.NotFound
            ? new NotFound()
            : account;
    }
}
