using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Birthsys.Identity.Api.ConfigOptions
{
    public class SwaggerConfigOptions(
        IApiVersionDescriptionProvider provider
    ) : IConfigureNamedOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = $"Birthsys Identity API {description.ApiVersion}",
                    Version = description.ApiVersion.ToString(),
                    Description = "API for managing user identities and authentication.",
                });
            }
            options.CustomSchemaIds(type => type.FullName);
            options.CustomOperationIds(apiDesc => apiDesc.ToString());
        }

        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }
    }
}