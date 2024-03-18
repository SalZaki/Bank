using Microsoft.AspNetCore.Http;

namespace Payment.Bank.Common.Exceptions;

public sealed class BadRequestException(string message, Exception? innerException = null)
    : CustomException(message, StatusCodes.Status400BadRequest, innerException);
