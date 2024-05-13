using System.Net;
using Payment.Bank.Common.Exceptions;

namespace Payment.Bank.Domain.Exceptions;

public abstract class DomainException(string message, HttpStatusCode statusCode)
    : CustomException(message, statusCode);
