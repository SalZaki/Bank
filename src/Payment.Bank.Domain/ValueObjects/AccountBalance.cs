using System.Globalization;
using Ardalis.GuardClauses;

namespace Payment.Bank.Domain.ValueObjects;

public sealed record AccountBalance
{
    public required decimal Value { get; init; }

    public static readonly AccountBalance Empty = Create(decimal.Zero);

    public static AccountBalance Create(decimal value)
    {
        Guard.Against.Null(value, nameof(value), "Value can not be null.");

        return new AccountBalance {Value = value};
    }

    public override string ToString()
    {
        return this.Value.ToString(CultureInfo.InvariantCulture);
    }

    public static implicit operator decimal(AccountBalance accountBalance) => accountBalance.Value;
}
