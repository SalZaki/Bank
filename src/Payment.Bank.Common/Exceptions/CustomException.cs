using System.Net;

namespace Payment.Bank.Common.Exceptions;

public abstract class CustomException(
    string message,
    HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
    Exception? innerException = null,
    params string[] errors)
    : Exception(message, innerException)
{
    public IEnumerable<string> Errors { get; } = errors;

    public HttpStatusCode StatusCode { get; } = statusCode;
}
