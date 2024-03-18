using Payment.Bank.Common.Exceptions;

namespace Payment.Bank.Domain.Exceptions;

public sealed class AccountNotFoundException() : NotFoundException("Account doesn't exist.");


