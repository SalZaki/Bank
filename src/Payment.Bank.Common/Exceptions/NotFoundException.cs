using Microsoft.AspNetCore.Http;

namespace Payment.Bank.Common.Exceptions;

public class NotFoundException(string message, Exception? innerException = null)
    : CustomException(message, StatusCodes.Status404NotFound, innerException);
