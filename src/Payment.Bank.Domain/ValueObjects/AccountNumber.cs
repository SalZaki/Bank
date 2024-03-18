using Ardalis.GuardClauses;
using Payment.Bank.Common.Utilities;

namespace Payment.Bank.Domain.ValueObjects;

public sealed record AccountNumber
{
    public required int Value { get; init; }

    public static readonly AccountNumber Empty = Create(int.MinValue);

    public static AccountNumber NewAccountNumber() => Create(RandomAccountDataGenerator.GenerateBankAccountNumber());

    public static AccountNumber Create(int value)
    {
        Guard.Against.Null(value, nameof(value), "Value can not be null.");

        return new AccountNumber {Value = value};
    }

    public override string ToString()
    {
        return this.Value.ToString();
    }

    public static implicit operator int(AccountNumber accountNumber) => accountNumber.Value;
}
