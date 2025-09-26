using Birthsys.Identity.Domain.Abstractions;

namespace Birthsys.Identity.Api.Middlewares
{
    public class ExceptionHandlerMiddleware(RequestDelegate next)
    {

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var error = Error.Build(500, "Internal Server Error", exception.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var jsonResponse = error.ToJsonString();
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}