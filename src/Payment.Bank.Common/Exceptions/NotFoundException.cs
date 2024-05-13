namespace Payment.Bank.Common.Exceptions;

public class NotFoundException(string message, Exception? innerException = null)
    : BadRequestException(message, innerException);
