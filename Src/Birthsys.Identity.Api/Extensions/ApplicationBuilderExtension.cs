using Birthsys.Identity.Api.Middlewares;

namespace Birthsys.Identity.Api.Extentions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}