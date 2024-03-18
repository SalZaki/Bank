namespace Payment.Bank.Common.Abstractions.Domain;

public interface IBusinessPolicy
{
    bool IsInvalid();

    string Message { get; }
}
