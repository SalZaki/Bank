using Microsoft.Extensions.Options;
using Payment.Bank.Api.Options;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Payment.Bank.Api.Middlewares;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Payment.Bank.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseLoggingMiddleware(this IApplicationBuilder app)
    {
        Guard.Against.Null(app, nameof(app));
        var apiOptions = app.ApplicationServices.GetRequiredService<IOptions<ApiOptions>>().Value;

        if (apiOptions.LatencyRequestLoggingEnabled is false)
        {
            return;
        }

        app.UseMiddleware<RequestLatencyLoggingMiddleware>();
    }

    public static void UseGlobalExceptionHandlingMiddleware(this IApplicationBuilder app)
    {
        Guard.Against.Null(app, nameof(app));
        var apiOptions = app.ApplicationServices.GetRequiredService<IOptions<ApiOptions>>().Value;

        if (apiOptions.LatencyRequestLoggingEnabled is false)
        {
            return;
        }

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    }

    public static void UseSwaggerEndpoints(this IApplicationBuilder app)
    {
        Guard.Against.Null(app, nameof(app));

        var swaggerOptions = app.ApplicationServices.GetRequiredService<IOptions<SwaggerOptions>>().Value;
        var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

        if (swaggerOptions.Enabled is false)
        {
            return;
        }

        app.UseSwagger(c =>
        {
            c.RouteTemplate = swaggerOptions.RouteTemplate;
        });

        app.UseSwaggerUI(c =>
        {
            c.DocExpansion(DocExpansion.List);
            c.RoutePrefix = swaggerOptions.RoutePrefix;

            foreach (var description in provider.ApiVersionDescriptions)
            {
                c.SwaggerEndpoint(string.Format(swaggerOptions.EndpointPath, description.GroupName), swaggerOptions.Name);
            }
        });
    }

    public static void UseCorsPolicy(this IApplicationBuilder applicationBuilder)
    {
        Guard.Against.Null(applicationBuilder, nameof(applicationBuilder));

        var corsOptions = applicationBuilder.ApplicationServices.GetRequiredService<IOptions<CorsOptions>>().Value;

        if (corsOptions.Enabled is false)
        {
            return;
        }

        if (corsOptions.Clients?.Length > 0)
        {
            applicationBuilder.UseCors(corsOptions.Name);
        }
    }
}
