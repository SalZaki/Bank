using Microsoft.AspNetCore.Http;

namespace Payment.Bank.Common.Exceptions;

public sealed class ForbiddenException(string message, Exception? innerException = null)
    : CustomException(message, StatusCodes.Status403Forbidden, innerException);
