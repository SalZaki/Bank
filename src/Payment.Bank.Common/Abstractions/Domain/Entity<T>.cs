using Payment.Bank.Common.Exceptions;

namespace Payment.Bank.Common.Abstractions.Domain;

public abstract record Entity<TId>(TId Id) : IEntity<TId>
{
    public DateTime? CreatedOnUtc { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    public string? ModifiedBy { get; set; }

    protected void CheckPolicy(IBusinessPolicy businessRule)
    {
        if (businessRule.IsInvalid())
        {
            throw new BusinessPolicyValidationException(businessRule);
        }
    }
}
