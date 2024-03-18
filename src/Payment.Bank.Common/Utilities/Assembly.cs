using System.Reflection;

namespace Payment.Bank.Common.Utilities;

public static class Assembly
{
    public static string? GetAssemblyVersion<T>()
    {
        var assembly = typeof(T).GetTypeInfo().Assembly;

        return assembly
            .GetCustomAttributes<AssemblyInformationalVersionAttribute>()
            .FirstOrDefault()?
            .InformationalVersion;
    }
}
