using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Payment.Bank.Api.Swagger;

public sealed class FeatureGateDocumentFilter(IFeatureManager featureManager) : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var apiDescription in context.ApiDescriptions)
        {
            var filterPipeline = apiDescription.ActionDescriptor.FilterDescriptors;
            var filterMetaData = filterPipeline
                .Select(filterInfo => filterInfo.Filter)
                .SingleOrDefault(filter => filter is FeatureGateAttribute);

            if (filterMetaData == default)
            {
                continue;
            }

            var featureGateAttribute = filterMetaData as FeatureGateAttribute;

            var isActive = featureManager
                .IsEnabledAsync(featureGateAttribute?.Features.Single())
                .GetAwaiter()
                .GetResult();

            if (isActive)
            {
                continue;
            }

            var apiPath = swaggerDoc.Paths.FirstOrDefault(o => o.Key.Contains(apiDescription?.RelativePath!));
            swaggerDoc.Paths.Remove(apiPath.Key);
        }
    }
}
