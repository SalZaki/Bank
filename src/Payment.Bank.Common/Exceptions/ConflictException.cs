using Microsoft.AspNetCore.Http;

namespace Payment.Bank.Common.Exceptions;

public sealed class ConflictException(string message, Exception? innerException = null)
    : CustomException(message, StatusCodes.Status409Conflict, innerException);
