using Ardalis.GuardClauses;
using Payment.Bank.Common.Abstractions.Domain;
using Payment.Bank.Domain.ValueObjects;

namespace Payment.Bank.Domain.Entities;

public sealed record Account : Aggregate<AccountId>, IComparable<Account>, IComparable
{
    public required AccountHolderName AccountHolderName { get; init; }

    public required AccountBalance AccountBalance { get; init; }

    public required AccountNumber AccountNumber { get; init; }

    public required AccountType AccountType { get; init; }

    public required SortCode SortCode { get; init; }

    public required Iban Iban { get; init; }

    public AccountStatus AccountStatus { get; private set; }

    public static readonly Account NotFound = Create(AccountId.Empty, AccountNumber.Empty, AccountHolderName.Empty);

    private Account(AccountId accountId, AccountStatus accountStatus) : base(accountId)
    {
        this.AccountStatus = accountStatus;
    }

    public static Account Create(
        AccountId accountId,
        AccountNumber accountNumber,
        AccountHolderName accountHolderName,
        AccountBalance? accountBalance = null,
        AccountType? accountType = null,
        SortCode? sortCode = null,
        Iban? iban = null,
        AccountStatus? accountStatus = null,
        string? createdBy = "System",
        DateTime? createdOnUtc = null)
    {
        Guard.Against.Null(accountNumber, nameof(accountNumber), "AccountNumber can not be null.");

        return new Account(accountId, accountStatus ?? AccountStatus.Active)
        {
            AccountNumber = accountNumber,
            AccountHolderName = accountHolderName,
            AccountBalance = accountBalance ?? AccountBalance.Empty,
            AccountType = accountType ?? AccountType.Current,
            SortCode = sortCode ?? SortCode.Empty,
            Iban = iban ?? Iban.Empty,
            CreatedBy = createdBy,
            CreatedOnUtc = createdOnUtc ?? DateTime.UtcNow
        };
    }

    public void Activate()
    {
        this.AccountStatus = AccountStatus.Active;
    }

    public void Deactivate()
    {
        this.AccountStatus = AccountStatus.Inactive;
    }

    public int CompareTo(Account? other)
    {
        if (other is null)
        {
            return 1;
        }

        return this.Id == other.Id ? 0 : 1;
    }

    public int CompareTo(object? obj)
    {
        if (obj is null)
        {
            return 1;
        }

        return obj is not Account account ? 1 : this.CompareTo(account);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Id);
    }
}
