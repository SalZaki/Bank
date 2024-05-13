using System.Net;

namespace Payment.Bank.Common.Exceptions;

public abstract class BadRequestException(string message, Exception? innerException = null)
    : CustomException(message, HttpStatusCode.BadRequest, innerException);
