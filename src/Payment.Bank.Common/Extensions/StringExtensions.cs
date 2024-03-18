namespace Payment.Bank.Common.Extensions;

public static class StringExtensions
{
    public static string? ToLowerSnakeCase(this string? str)
    {
        return str == null
            ? null
            : string
                .Concat(str.Trim()
                    .Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString()))
                .ToLower();
    }
}
