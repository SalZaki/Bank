using System.Collections.ObjectModel;
using Ardalis.GuardClauses;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Payment.Bank.Common.Abstractions.Commands;
using Payment.Bank.Application.Accounts.Repositories;
using Payment.Bank.Common.Exceptions;
using Payment.Bank.Domain.Entities;
using Payment.Bank.Domain.ValueObjects;
using OneOf;
using OneOf.Types;

namespace Payment.Bank.Application.Accounts.Features.ActivateAccount.v1;

public class ActivateAccountHandler(
    IAccountRepository accountRepository,
    FluentValidation.IValidator<ActivateAccountCommand> validator,
    ILogger<ActivateAccountHandler> logger)
    : ICommandHandler<ActivateAccountCommand, OneOf<ActivateAccountResponse, ReadOnlyCollection<ValidationFailure>, NotFound>>
{
    private readonly IAccountRepository _accountRepository =
        Guard.Against.Null(accountRepository, nameof(accountRepository));

    private readonly FluentValidation.IValidator<ActivateAccountCommand> _validator =
        Guard.Against.Null(validator, nameof(validator));

    private readonly ILogger _logger =
        Guard.Against.Null(logger, nameof(logger));

    public async Task<OneOf<ActivateAccountResponse, ReadOnlyCollection<ValidationFailure>, NotFound>> HandleAsync(
        ActivateAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(command, nameof(command));

        var validationResult = await this._validator.ValidateAsync(command, cancellationToken).ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return validationResult.Errors.AsReadOnly();
        }

        var result = await this.ActivateAccountInternal(command, cancellationToken);

        return result.Match<OneOf<ActivateAccountResponse, ReadOnlyCollection<ValidationFailure>, NotFound>>(
            activateAccountResponse => activateAccountResponse,
            notFound => notFound);
    }

    private async Task<OneOf<ActivateAccountResponse, NotFound>> ActivateAccountInternal(
        ActivateAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var accountResult =
                await this.GetAccountAsync(AccountNumber.Create(command.AccountNumber), cancellationToken);

            if (accountResult.TryPickT1(out var notFound, out var account))
            {
                return notFound;
            }

            account.Activate();

            await this._accountRepository.UpdateAsync(account, cancellationToken);

            return new ActivateAccountResponse(account.AccountStatus);
        }
        catch (CustomException ex)
        {
            this._logger.Log(LogLevel.Error, "{Message}, {StatusCode}", ex.Message, ex.StatusCode);

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
