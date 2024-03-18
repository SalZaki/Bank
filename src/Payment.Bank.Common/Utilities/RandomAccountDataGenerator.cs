namespace Payment.Bank.Common.Utilities;

public static class RandomAccountDataGenerator
{
    public static int GenerateInt(int min, int max) => new Random().Next(min, max);

    public static int GenerateBankAccountNumber() => GenerateInt(10000000, 99999999);

    public static int GenerateBankSortCode() => GenerateInt(100000, 999999);

    public static string GenerateIban() => "GB" + GenerateInt(10000000, 99999999);
}
