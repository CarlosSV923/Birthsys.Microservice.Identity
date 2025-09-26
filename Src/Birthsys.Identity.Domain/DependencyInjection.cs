using Microsoft.Extensions.DependencyInjection;

namespace Birthsys.Identity.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            // Register domain services here if any
            return services;
        }
    }
}