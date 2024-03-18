using Payment.Bank.Common.Extensions;

namespace Payment.Bank.Application;

public static class ErrorCodes
{
    private const string RequiredSuffix = "required";

    private const string InvalidSuffix = "invalid";

    private const string AlreadyExistsSuffix = "already_exists";

    private const string NotFoundSuffix = "not_found";

    public static string AlreadyExists(string propertyName) => $"{propertyName.ToLowerSnakeCase()}_{AlreadyExistsSuffix}";

    public static string NotFound(string propertyName) => $"{propertyName.ToLowerSnakeCase()}_{NotFoundSuffix}";

    public static string Required(string propertyName) => $"{propertyName.ToLowerSnakeCase()}_{RequiredSuffix}";

    public static string Invalid(string propertyName) => $"{propertyName.ToLowerSnakeCase()}_{InvalidSuffix}";
}
