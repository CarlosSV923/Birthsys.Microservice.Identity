using Birthsys.Identity.Api.Middlewares;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Newtonsoft.Json;

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

        public static IApplicationBuilder MapCustomHealthCheck(this WebApplication app)
        {
            app.MapHealthChecks(
                "/health", new HealthCheckOptions
                {
                    ResponseWriter = async (context, report) =>
                    {
                        context.Response.ContentType = "application/json";

                        var result = new
                        {
                            status = report.Status.ToString(),
                            checks = report.Entries.Select(e => new
                            {
                                name = e.Key,
                                status = e.Value.Status.ToString(),
                                description = e.Value.Description
                            }),
                            totalDuration = report.TotalDuration.TotalMilliseconds
                        };

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                    }
                }
            );

            return app;

        }
    }
}