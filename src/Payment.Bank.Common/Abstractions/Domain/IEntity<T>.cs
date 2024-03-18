namespace Payment.Bank.Common.Abstractions.Domain;

public interface IEntity<T> : IEntity
{
    public T Id { get; init; }
}
