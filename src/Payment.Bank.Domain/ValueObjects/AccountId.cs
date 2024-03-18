using Ardalis.GuardClauses;

namespace Payment.Bank.Domain.ValueObjects;

public sealed record AccountId
{
    public required Guid Value { get; init; }

    public static readonly AccountId Empty = Create(Guid.Empty);

    public static AccountId NewAccountId() => Create(Guid.NewGuid());

    public static AccountId Create(Guid value)
    {
        Guard.Against.Null(value, nameof(value), "Value can not be null.");

        return new AccountId {Value = value};
    }

    public override string ToString()
    {
        return this.Value.ToString();
    }

    public static implicit operator Guid(AccountId accountId) => accountId.Value;
}
