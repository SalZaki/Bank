using FluentValidation.Results;
using OneOf;
using OneOf.Types;
using Payment.Bank.Application.Accounts.Features.ActivateAccount.v1;
using Payment.Bank.Application.Accounts.Features.CreateAccount.v1;
using Payment.Bank.Application.Accounts.Features.DeactivateAccount.v1;
using Payment.Bank.Application.Accounts.Features.GetAccount.v1;

namespace Payment.Bank.Application.Accounts.Services;

public interface IAccountService
{
    Task<OneOf<GetAccountResponse, ValidationResult, NotFound>> GetAccountAsync(GetAccountQuery query, CancellationToken cancellationToken = default);

    Task<OneOf<CreateAccountResponse, ValidationResult>> CreateAccountAsync(CreateAccountCommand command, CancellationToken cancellationToken = default);

    Task<OneOf<ActivateAccountResponse, ValidationResult, NotFound>> ActivateAccountAsync(ActivateAccountCommand command, CancellationToken cancellationToken = default);

    Task<OneOf<DeactivateAccountResponse, ValidationResult, NotFound>> DeactivateAccountAsync(DeactivateAccountCommand command, CancellationToken cancellationToken = default);
}
