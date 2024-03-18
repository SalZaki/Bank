namespace Payment.Bank.Domain.ValueObjects;

public sealed record AccountType
{
    public static readonly AccountType Saving = new("Saving");

    public static readonly AccountType Current = new("Current");

    public string Value { get; }

    private AccountType(string value)
    {
        this.Value = value;
    }

    public override string ToString()
    {
        return this.Value;
    }

    public static implicit operator string(AccountType accountType) => accountType.Value;
}
