using System.Net;

namespace Payment.Bank.Common.Exceptions;

public sealed class ConflictException(string message, Exception? innerException = null)
    : CustomException(message, HttpStatusCode.Conflict, innerException);
