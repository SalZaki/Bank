using Payment.Bank.Common.Exceptions;

namespace Payment.Bank.Domain.Exceptions;

public sealed class AccountNotFoundException(string accountId)
    : NotFoundException($"No account found with the Id: {accountId}.");
