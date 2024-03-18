namespace Payment.Bank.Common.Abstractions.Queries;

public interface IQueryHandler<in TQuery, TResult> where TQuery : class, IQuery<TResult>
    where TResult : notnull
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}
