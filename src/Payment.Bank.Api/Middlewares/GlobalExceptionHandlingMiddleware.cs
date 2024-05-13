using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Payment.Bank.Api.Options;

namespace Payment.Bank.Api.Middlewares;

public sealed class GlobalExceptionHandlingMiddleware(
    IOptions<ApiOptions> apiOptions,
    IHostEnvironment hostingEnvironment,
    RequestDelegate next,
    ILogger<GlobalExceptionHandlingMiddleware> logger)
{
    private readonly ApiOptions _apiOptions = apiOptions.Value;
    private readonly ILogger _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }

#pragma warning disable CA1031
        catch (Exception ex)
#pragma warning restore CA1031
        {
            this._logger.Log(LogLevel.Error, ex, "Exception: {message}", ex.Message);
            await this.HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var routeData = context.GetRouteData();
        var controllerName = routeData.Values["controller"]?.ToString()?.ToLowerInvariant();
        var problemDetails = this.CreateProblemDetails(ex, controllerName);

        var response = context.Response;
        response.ContentType = Constants.MimeTypes.ApplicationProblemJson;

        if (problemDetails.Status != null)
        {
            response.StatusCode = problemDetails.Status.Value;
        }

        await response.WriteAsync(JsonSerializer.Serialize(problemDetails));
    }

    private ProblemDetails CreateProblemDetails(Exception ex, string? controllerName)
    {
        ProblemDetails problemDetails = ex switch
        {
            UnauthorizedAccessException unauthorizedAccessException => new ProblemDetails
            {
                Title = Constants.Errors.UnauthorizedAccess.ErrorTitle,
                Detail = Constants.Errors.UnauthorizedAccess.ErrorMessage + unauthorizedAccessException.Message,
                Status = StatusCodes.Status401Unauthorized,
                Extensions =
                {
                    {"documentation_url", this._apiOptions.DocumentationUrl + $"{controllerName}/{Constants.Errors.UnauthorizedAccess.ErrorCode}"},
                    {"stack_trace", unauthorizedAccessException.StackTrace},
                }
            },
            _ => new ProblemDetails
            {
                Title = Constants.Errors.Server.ErrorTitle,
                Detail = Constants.Errors.Server.ErrorMessage,
                Status = StatusCodes.Status500InternalServerError,
                Extensions =
                {
                    {"documentation_url", this._apiOptions.DocumentationUrl + $"{controllerName}/{Constants.Errors.Server.ErrorCode}"},
                    {"stack_trace", ex.StackTrace},
                }
            }
        };

        // No need to leak stack trace on PRD.
        if (hostingEnvironment.IsProduction())
        {
            problemDetails.Extensions["stack_trace"] = string.Empty;
        }

        return problemDetails;
    }
}
