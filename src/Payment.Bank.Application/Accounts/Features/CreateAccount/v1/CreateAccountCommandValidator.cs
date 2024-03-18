using Ardalis.GuardClauses;
using FluentValidation;
using Payment.Bank.Application.Accounts.Repositories;
using Payment.Bank.Domain.Entities;
using Payment.Bank.Domain.ValueObjects;

namespace Payment.Bank.Application.Accounts.Features.CreateAccount.v1;

public sealed class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    private readonly IAccountRepository _accountRepository;

    public CreateAccountCommandValidator(IAccountRepository accountRepository)
    {
        this._accountRepository = Guard.Against.Null(accountRepository, nameof(accountRepository));

        this.RuleFor(x => x.AccountHolderName)
            .NotEmpty()
            .WithErrorCode(ErrorCodes.Required(nameof(AccountHolderName)))
            .WithMessage("Account holder's name is required.");

        this.RuleFor(x => x.AccountNumber)
            .NotEmpty()
            .WithErrorCode(ErrorCodes.Required(nameof(AccountNumber)))
            .WithMessage("Account number is required.");

        this.RuleFor(x => x.AccountNumber)
            .GreaterThan(0)
            .WithErrorCode(ErrorCodes.Invalid(nameof(AccountNumber)))
            .WithMessage("Account number can not be zero or negative");

        this.RuleFor(x => x.AccountNumber)
            .MustAsync(this.AccountNumberExistsAsync)
            .WithErrorCode(ErrorCodes.AlreadyExists(nameof(AccountNumber)))
            .WithMessage(x => $"Account already exists with number: {x.AccountNumber}");
    }

    private async Task<bool> AccountNumberExistsAsync(int? accountNumber, CancellationToken cancellationToken = default)
    {
        var account = await this._accountRepository.FindOneAsync(x => x.AccountNumber == accountNumber, cancellationToken);

        return account == Account.NotFound;
    }
}
