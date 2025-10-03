using Birthsys.Identity.Application.Behaviors;
using Birthsys.Identity.Application.Events;
using Birthsys.Identity.Application.Options;
using Birthsys.Identity.Application.Producers;
using Birthsys.Identity.Application.Services.Jwt;
using Birthsys.Identity.Application.Services.PasswordHasher;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Birthsys.Identity.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.Configure<HashingOptions>(configuration.GetSection(HashingOptions.SectionName));
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            services.Configure<PasswordOptions>(configuration.GetSection(PasswordOptions.SectionName));

            services.AddScoped<IEventsPublisherProvider, EventsPublisherProvider>();

            services.AddTransient<IJwtProvider, JwtProvider>();
            services.AddTransient<IPasswordHasherProvider, PasswordHasherProvider>();

            services.AddTransient(typeof(IProducer<>), typeof(Producer<>));

            
            return services;
        }
    }
}