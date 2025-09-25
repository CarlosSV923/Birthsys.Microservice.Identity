using Birthsys.Identity.Application.Events;
using Birthsys.Identity.Application.Options;
using Birthsys.Identity.Application.Services.Jwt;
using Birthsys.Identity.Application.Services.PasswordHasher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Birthsys.Identity.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            services.AddScoped<IEventsPublisherProvider, EventsPublisherProvider>();
            services.Configure<HashingOptions>(configuration.GetSection(HashingOptions.SectionName));
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            services.AddTransient<IJwtProvider, JwtProvider>();
            services.AddTransient<IPasswordHasherProvider, PasswordHasherProvider>();
            return services;
        }
    }
}