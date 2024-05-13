using System.Collections.ObjectModel;
using Ardalis.GuardClauses;
using FluentValidation.Results;
using Payment.Bank.Common.Abstractions.Queries;
using Payment.Bank.Common.Exceptions;
using Payment.Bank.Application.Accounts.Repositories;
using Payment.Bank.Common.Mappers;
using Payment.Bank.Domain.Entities;
using Payment.Bank.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace Payment.Bank.Application.Accounts.Features.GetAccount.v1;

public class GetAccountHandler(
    IAccountRepository accountRepository,
    IMapper<Account, GetAccountResponse> mapper,
    FluentValidation.IValidator<GetAccountQuery> getAccountQueryValidator,
    ILogger<GetAccountHandler> logger)
    : IQueryHandler<GetAccountQuery, OneOf<GetAccountResponse, ReadOnlyCollection<ValidationFailure>, NotFound>>
{
    private readonly IAccountRepository _accountRepository = Guard.Against.Null(accountRepository, nameof(accountRepository));
    private readonly IMapper<Account, GetAccountResponse> _mapper = Guard.Against.Null(mapper, nameof(mapper));
    private readonly FluentValidation.IValidator<GetAccountQuery> _validator = Guard.Against.Null(getAccountQueryValidator, nameof(getAccountQueryValidator));
    private readonly ILogger _logger = Guard.Against.Null(logger, nameof(logger));

    public async Task<OneOf<GetAccountResponse, ReadOnlyCollection<ValidationFailure>, NotFound>> HandleAsync(
        GetAccountQuery query,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(query, nameof(query));

        var validationResult = await this._validator.ValidateAsync(query, cancellationToken).ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return validationResult.Errors.AsReadOnly();
        }

        var accountNumber = AccountNumber.Create(query.AccountNumber);

        var result = await this.GetAccountInternal(accountNumber, cancellationToken).ConfigureAwait(false);

        return result.Match<OneOf<GetAccountResponse, ReadOnlyCollection<ValidationFailure>, NotFound>>(
            getUserResponse => getUserResponse,
            notFound => notFound);
    }

    private async ValueTask<OneOf<GetAccountResponse, NotFound>> GetAccountInternal(
        AccountNumber accountNumber,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var account = await this._accountRepository
                .FindOneAsync(x => x.AccountNumber == accountNumber, cancellationToken)
                .ConfigureAwait(false);

            if (account != Account.NotFound)
            {
                return this._mapper.Map(account);
            }

            this._logger.Log(LogLevel.Warning, "Account with account number {accountNumber} does not exist", accountNumber);

            return new NotFound();
        }
        catch (CustomException ex)
        {
            this._logger.Log(LogLevel.Error, "{Message}, {StatusCode}", ex.Message, ex.StatusCode);

            throw;
        }
    }
}
