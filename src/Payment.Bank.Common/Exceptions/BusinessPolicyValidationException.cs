using Payment.Bank.Common.Abstractions.Domain;

namespace Payment.Bank.Common.Exceptions;

public sealed class BusinessPolicyValidationException(IBusinessPolicy businessPolicy)
    : DomainException(businessPolicy.Message)
{
    public IBusinessPolicy InvalidPolicy { get; } = businessPolicy;

    public string Detail { get; } = businessPolicy.Message;

    public override string ToString()
    {
        return $"{this.InvalidPolicy.GetType().FullName}: {this.InvalidPolicy.Message}";
    }
}
