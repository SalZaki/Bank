using Microsoft.AspNetCore.Http;

namespace Payment.Bank.Common.Exceptions;

public abstract class CustomException(
    string message,
    int statusCode = StatusCodes.Status500InternalServerError,
    Exception? innerException = null,
    params string[] errors)
    : Exception(message, innerException)
{
    public IEnumerable<string> ErrorMessages { get; protected set; } = errors;

    public int StatusCode { get; protected set; } = statusCode;
}
