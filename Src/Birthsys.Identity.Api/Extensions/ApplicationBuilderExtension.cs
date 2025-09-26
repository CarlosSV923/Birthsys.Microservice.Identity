using Birthsys.Identity.Api.Middlewares;

namespace Birthsys.Identity.Api.Extentions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlerMiddleware>();
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var descriptions = app.DescribeApiVersions();
                foreach (var groupName in descriptions.Select(item => item.GroupName))
                {
                    var url = $"/swagger/{groupName}/swagger.json";
                    var name = groupName.ToUpperInvariant();
                    options.SwaggerEndpoint(url, name);
                }

            });
            return app;
        }
    }
}