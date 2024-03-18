namespace Payment.Bank.Common;

// public sealed record Error(string Code, string Message)
// {
//     public static implicit operator string(Error error) => error.Code;
//
//     public override int GetHashCode() => HashCode.Combine(this.Code, this.Message);
//
//     public static Error Empty => new(string.Empty, string.Empty);
//
//     public bool Equals(Error? other)
//     {
//         if (other is null)
//         {
//             return false;
//         }
//
//         return this.Code == other.Code && this.Message == other.Message;
//     }
// }

public sealed class Error(ErrorType errorType, params string[] errorCodes)
{
    public ErrorType ErrorType { get; } = errorType;

    public IEnumerable<string> ErrorCodes { get; } = errorCodes;
}

public enum ErrorType
{
    UnprocessableEntity,
    BadRequest,
    Conflict,
    NotFound,
    Unknown,
    Forbidden,
    NotSupported,
    InternalServerError,
}
