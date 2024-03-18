using Ardalis.GuardClauses;
using Payment.Bank.Common.Utilities;

namespace Payment.Bank.Domain.ValueObjects;

public sealed record SortCode
{
    public required int Value { get; init; }

    public static readonly SortCode Empty = Create(int.MinValue);

    public static SortCode NewSortCode() => Create(RandomAccountDataGenerator.GenerateBankSortCode());

    public static SortCode Create(int value)
    {
        Guard.Against.Null(value, nameof(value), "Value can not be null.");

        return new SortCode {Value = value};
    }

    public override string ToString()
    {
        return this.Value.ToString();
    }

    public static implicit operator int(SortCode sortCode) => sortCode.Value;
}
