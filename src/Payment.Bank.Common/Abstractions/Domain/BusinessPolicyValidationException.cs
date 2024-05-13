namespace Payment.Bank.Common.Abstractions.Domain;

public sealed class BusinessPolicyValidationException(IBusinessPolicy businessPolicy)
    : Exception(businessPolicy.Message)
{
    public IBusinessPolicy InvalidPolicy { get; } = businessPolicy;
    
    public override string ToString()
    {
        return $"{this.InvalidPolicy.GetType().FullName}: {this.InvalidPolicy.Message}";
    }
}
