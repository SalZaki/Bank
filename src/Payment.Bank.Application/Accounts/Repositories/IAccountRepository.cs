using Payment.Bank.Common.Abstractions.Data;
using Payment.Bank.Domain.Entities;
using Payment.Bank.Domain.ValueObjects;

namespace Payment.Bank.Application.Accounts.Repositories;

public interface IAccountRepository : IRepository<Account, AccountId>;
