using Payment.Bank.Common.Exceptions;

namespace Payment.Bank.Domain.Exceptions;

public sealed class InvalidAccountHolderNameException(string accountHolderName)
    : BadRequestException($"Account holder's name: '{accountHolderName}' is invalid.");
