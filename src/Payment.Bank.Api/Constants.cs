namespace Payment.Bank.Api;

public static class Constants
{
    public static class Configuration
    {
        public const string ApiSectionName = "Api";

        public const string CorsSectionName = "Cors";

        public const string SwaggerSectionName = "Swagger";

        public const string FeatureFlagsSection = "FeatureFlags";
    }

    public static class MimeTypes
    {
        public const string ApplicationJson = "application/json";

        public const string ApplicationProblemJson = "application/problem+json";
    }

    public static class Errors
    {
        public static class Server
        {
            public const string ErrorTitle = "Server Error";

            public const string ErrorCode = "10001";

            public const string ErrorMessage = "Bank Api is unavailable and returned an internal error code. Please contact system administrator or try again later";
        }

        public static class Validation
        {
            public const string ErrorTitle = "Validation Error";

            public const string ErrorCode = "20001";

            public const string ErrorMessage = "Bank Api validation has failed, due to following error(s)";
        }

        public static class UnauthorizedAccess
        {
            public const string ErrorTitle = "Access Denied";

            public const string ErrorCode = "30001";

            public const string ErrorMessage = "You do not have permission to perform this action or access this resource.";
        }
    }

    public static class Features
    {
        public const string GetAccount = "GetAccount";

        public const string CreateAccount = "CreateAccount";

        public const string DeactivateAccount = "DeactivateAccount";

        public const string ActivateAccount = "ActivateAccount";
    }
}
