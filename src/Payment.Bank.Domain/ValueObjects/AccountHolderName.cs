using Payment.Bank.Domain.Exceptions;

namespace Payment.Bank.Domain.ValueObjects;

public sealed record AccountHolderName
{
    private const int MaxLength = 100;

    private const int MinLength = 2;

    public string Value { get; }

    public static readonly AccountHolderName Empty = Create(nameof(Empty));

    private AccountHolderName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidAccountHolderNameException(value);
        }

        if (string.IsNullOrWhiteSpace(value) || value.Length is > MaxLength or < MinLength)
        {
            throw new InvalidAccountHolderNameException(value);
        }

        this.Value = value.Trim();
    }

    public static AccountHolderName Create(string value)
    {
        return new AccountHolderName(value);
    }

    public override string ToString()
    {
        return this.Value;
    }

    public static implicit operator string(AccountHolderName accountHolderName) => accountHolderName.Value;
}
