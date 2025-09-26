using Birthsys.Identity.Api.ConfigOptions;

namespace Birthsys.Identity.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.ConfigureOptions<SwaggerConfigOptions>();
            return services;
        }

        public static IServiceCollection AddHealthCheckConfig(this IServiceCollection services)
        {
            services.AddHealthChecks();
            return services;
        }

        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication();
            services.AddAuthorization();
            services.ConfigureOptions<JwtConfigOptions>();
            return services;
        }
    }
}