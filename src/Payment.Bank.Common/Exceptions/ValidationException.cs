using Microsoft.AspNetCore.Http;

namespace Payment.Bank.Common.Exceptions;

public sealed class ValidationException(string message, Exception? innerException = null, params string[] errors)
    : CustomException(message, StatusCodes.Status400BadRequest, innerException, errors);
