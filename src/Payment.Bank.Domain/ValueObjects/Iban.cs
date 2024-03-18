using Ardalis.GuardClauses;
using Payment.Bank.Common.Utilities;

namespace Payment.Bank.Domain.ValueObjects;

public sealed record Iban
{
    public required string Value { get; init; }

    public static readonly Iban Empty = Create(nameof(Empty));

    public static Iban NewIban() => Create(RandomAccountDataGenerator.GenerateIban());

    public static Iban Create(string value)
    {
        Guard.Against.Null(value, nameof(value), "Value can not be null.");

        return new Iban {Value = value};
    }

    public override string ToString()
    {
        return this.Value;
    }

    public static implicit operator string(Iban iban) => iban.Value;
}
