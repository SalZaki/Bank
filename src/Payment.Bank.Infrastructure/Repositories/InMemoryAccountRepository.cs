using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq.Expressions;
using Ardalis.GuardClauses;
using Payment.Bank.Application.Accounts.Repositories;
using Payment.Bank.Domain.Entities;
using Payment.Bank.Domain.ValueObjects;

namespace Payment.Bank.Infrastructure.Repositories;

public sealed class InMemoryAccountRepository : IAccountRepository
{
  private readonly ConcurrentDictionary<Guid, Account> _accounts = new();

  public Task<Account> AddAsync(Account account, CancellationToken cancellationToken = default)
  {
    Guard.Against.Null(account, nameof(account));

    this._accounts.TryAdd<Guid, Account>(account.Id, account);

    return Task.FromResult(account);
  }

  public Task<Account> UpdateAsync(Account account, CancellationToken cancellationToken = default)
  {
    Guard.Against.Null(account, nameof(account));

    if (this._accounts.ContainsKey(account.Id))
    {
        this._accounts[account.Id] = account;
    }

    return Task.FromResult(account);
  }

  public Task<IReadOnlyList<Account>> UpdateAsync(Expression<Func<Account, bool>> predicate,
    CancellationToken cancellationToken = default)
  {
    Guard.Against.Null(predicate, nameof(predicate));

    var result = this._accounts
      .Select(x => x.Value)
      .Where(predicate.Compile())
      .ToList();

    foreach (var wallet in result)
    {
        this._accounts[wallet.Id] = wallet;
    }

    return Task.FromResult<IReadOnlyList<Account>>(result.ToImmutableList());
  }

  public Task DeleteAsync(Expression<Func<Account, bool>> predicate, CancellationToken cancellationToken = default)
  {
    Guard.Against.Null(predicate, nameof(predicate));

    var result = this._accounts
      .Select(x => x.Value)
      .Where(predicate.Compile());

    foreach (var account in result)
    {
        this._accounts.Remove(account.Id, out _);
    }

    return Task.CompletedTask;
  }

  public Task DeleteAsync(Account account, CancellationToken cancellationToken = default)
  {
    Guard.Against.Null(account, nameof(account));

    this._accounts.Remove(account.Id, out _);

    return Task.CompletedTask;
  }

  public Task DeleteByIdAsync(AccountId id, CancellationToken cancellationToken = default)
  {
    Guard.Against.Null(id, nameof(id));

    this._accounts.Remove(id, out _);

    return Task.CompletedTask;
  }

  public Task DeleteRangeAsync(IReadOnlyList<Account> entities, CancellationToken cancellationToken = default)
  {
    Guard.Against.Null(entities, nameof(entities));

    foreach (var entity in entities)
    {
        this._accounts.Remove(entity.Id, out _);
    }

    return Task.CompletedTask;
  }

  public Task<IReadOnlyList<Account>> GetAllAsync(CancellationToken cancellationToken = default)
  {
    var result = this._accounts
      .Select(x => x.Value)
      .ToImmutableList();

    return Task.FromResult<IReadOnlyList<Account>>(result);
  }

  public Task<Account> FindByIdAsync(AccountId id, CancellationToken cancellationToken = default)
  {
    Guard.Against.Null(id, nameof(id));

    var result = this._accounts.FirstOrDefault(x => x.Key == id).Value;

    return Task.FromResult(result ?? Account.NotFound);
  }

  public Task<Account> FindOneAsync(Expression<Func<Account, bool>> predicate, CancellationToken cancellationToken = default)
  {
    Guard.Against.Null(predicate, nameof(predicate));

    var result = this._accounts
      .Select(x => x.Value)
      .Where(predicate.Compile())
      .FirstOrDefault();

    return Task.FromResult(result ?? Account.NotFound);
  }

  public Task<IReadOnlyList<Account>> FindAsync(Expression<Func<Account, bool>> predicate, CancellationToken cancellationToken = default)
  {
    Guard.Against.Null(predicate, nameof(predicate));

    var result = this._accounts
      .Select(x => x.Value)
      .Where(predicate.Compile())
      .ToImmutableList();

    return Task.FromResult<IReadOnlyList<Account>>(result);
  }
}
