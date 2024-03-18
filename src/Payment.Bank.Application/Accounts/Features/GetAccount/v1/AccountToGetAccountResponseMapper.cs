using Ardalis.GuardClauses;
using Payment.Bank.Common.Mappers;
using Payment.Bank.Domain.Entities;

namespace Payment.Bank.Application.Accounts.Features.GetAccount.v1;

public class AccountToGetAccountResponseMapper : IMapper<Account, GetAccountResponse>
{
    public GetAccountResponse Map(Account source)
    {
        Guard.Against.Null(source, nameof(source));

        return new GetAccountResponse
        {
            AccountId = source.Id.ToString(),
            AccountBalance = source.AccountBalance.Value,
            AccountNumber = source.AccountNumber.Value,
            AccountType = source.AccountType.Value,
            AccountHolderName = source.AccountHolderName.Value,
            SortCode = source.SortCode.Value,
            Iban = source.Iban.Value,
            AccountStatus = source.AccountStatus.Value,
            CreatedBy = source.CreatedBy,
            CreatedOn = source.CreatedOnUtc
        };
    }
}
