using FluentValidation;
using Payment.Bank.Domain.ValueObjects;

namespace Payment.Bank.Application.Accounts.Features.GetAccount.v1;

public sealed class GetAccountQueryValidator : AbstractValidator<GetAccountQuery>
{
    public GetAccountQueryValidator()
    {
        this.RuleFor(x => x.AccountNumber)
            .NotEmpty()
            .WithErrorCode(ErrorCodes.Required(nameof(AccountNumber)))
            .WithMessage("Account number is required.");

        this.RuleFor(x => x.AccountNumber)
            .GreaterThan(0)
            .WithErrorCode(ErrorCodes.Invalid(nameof(AccountNumber)))
            .WithMessage("Account number can not be zero or negative");
    }
}
