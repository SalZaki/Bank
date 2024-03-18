namespace Payment.Bank.Common.Exceptions;
using Microsoft.AspNetCore.Http;

public class DomainException(
    string message,
    int statusCode = StatusCodes.Status400BadRequest,
    Exception? innerException = null)
    : CustomException(message, statusCode, innerException);
