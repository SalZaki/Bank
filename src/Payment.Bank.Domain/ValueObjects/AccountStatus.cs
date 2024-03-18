namespace Payment.Bank.Domain.ValueObjects;

public sealed record AccountStatus
{
    public static readonly AccountStatus Active = new("Active");

    public static readonly AccountStatus Inactive = new("Inactive");

    public string Value { get; }

    private AccountStatus(string value)
    {
        this.Value = value;
    }

    public override string ToString()
    {
        return this.Value;
    }

    public static implicit operator string(AccountStatus accountStatus) => accountStatus.Value;
}
