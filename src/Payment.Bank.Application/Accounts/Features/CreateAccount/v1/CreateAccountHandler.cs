using System.Collections.ObjectModel;
using Ardalis.GuardClauses;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Payment.Bank.Application.Accounts.Repositories;
using Payment.Bank.Common.Abstractions.Commands;
using Payment.Bank.Domain.Entities;
using OneOf;
using Payment.Bank.Common.Exceptions;
using Payment.Bank.Domain.ValueObjects;

namespace Payment.Bank.Application.Accounts.Features.CreateAccount.v1;

public class CreateAccountHandler(
    IAccountRepository accountRepository,
    FluentValidation.IValidator<CreateAccountCommand> validator,
    ILogger<CreateAccountHandler> logger)
    : ICommandHandler<CreateAccountCommand, OneOf<CreateAccountResponse, ReadOnlyCollection<ValidationFailure>>>
{
    private readonly IAccountRepository _accountRepository = Guard.Against.Null(accountRepository, nameof(accountRepository));
    private readonly FluentValidation.IValidator<CreateAccountCommand> _validator = Guard.Against.Null(validator, nameof(validator));
    private readonly ILogger _logger = Guard.Against.Null(logger, nameof(logger));

    public async Task<OneOf<CreateAccountResponse, ReadOnlyCollection<ValidationFailure>>> HandleAsync(
        CreateAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(command, nameof(command));

        var validationResult = await this._validator.ValidateAsync(command, cancellationToken).ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return validationResult.Errors.AsReadOnly();
        }

        return await this.CreateAccountInternal(command, cancellationToken);
    }

    private static AccountType MapAccountType(string? accountType)
    {
        return accountType switch
        {
            "01" => AccountType.Current,
            "02" => AccountType.Saving,
            _ => AccountType.Current
        };
    }

    private async ValueTask<CreateAccountResponse> CreateAccountInternal(
        CreateAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var account = Account.Create(
                command.AccountId.HasValue ? AccountId.Create(command.AccountId.Value) : AccountId.NewAccountId(),
                command.AccountNumber.HasValue
                    ? AccountNumber.Create(command.AccountNumber.Value)
                    : AccountNumber.NewAccountNumber(),
                AccountHolderName.Create(command.AccountHolderName),
                command.AccountBalance.HasValue
                    ? AccountBalance.Create(command.AccountBalance.Value)
                    : AccountBalance.Empty,
                MapAccountType(command.AccountType),
                command.SortCode.HasValue ? SortCode.Create(command.SortCode.Value) : SortCode.NewSortCode(),
                string.IsNullOrEmpty(command.Iban) ? Iban.Create(command.Iban!) : Iban.NewIban(),
                AccountStatus.Active);

            await this._accountRepository.AddAsync(account, cancellationToken);

            return new CreateAccountResponse {AccountNumber = account.AccountNumber};
        }
        catch (CustomException ex)
        {
            this._logger.Log(LogLevel.Error, "{Message}, {StatusCode}", ex.Message, ex.StatusCode);

            throw;
        }
    }
}
