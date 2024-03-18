using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Payment.Bank.Api.Extensions;

public static class ValidationResultExtensions
{
    public static BadRequest<ProblemDetails> ToBadRequest(this ValidationResult validationResult, string? documentationUrl)
    {
        var problemDetails = new ProblemDetails
        {
            Type = $"https://httpstatuses.io/{StatusCodes.Status400BadRequest}",
            Title = Constants.Errors.Validation.ErrorTitle,
            Detail = Constants.Errors.Validation.ErrorMessage,
            Status = StatusCodes.Status400BadRequest,
            Extensions =
            {
                {"errors", validationResult.Errors.Select(e => new {Name = e.PropertyName, Message = e.ErrorMessage})},
            }
        };

        if (!string.IsNullOrWhiteSpace(documentationUrl))
        {
            problemDetails.Extensions["documentation_url"] = $"{documentationUrl}/account/{Constants.Errors.Validation.ErrorCode}";
        }

        return TypedResults.BadRequest(problemDetails);
    }
}

