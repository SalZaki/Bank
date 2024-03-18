namespace Payment.Bank.Common.Abstractions.Domain;

public abstract record Aggregate<TId>(TId Id) : Entity<TId>(Id), IAggregate<TId>;

