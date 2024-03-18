namespace Payment.Bank.Common.Abstractions.Domain;

public interface IAggregate<T> : IAggregate, IEntity<T>;
