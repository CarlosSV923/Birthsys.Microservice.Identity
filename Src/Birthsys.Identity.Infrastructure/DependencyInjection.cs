using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Domain.Aggregates.UserEvents.Interfaces;
using Birthsys.Identity.Domain.Aggregates.Users.Interfaces;
using Birthsys.Identity.Infrastructure.Broker;
using Birthsys.Identity.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Birthsys.Identity.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Database.Context>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<Database.Context>());

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserEventRepository, UserEventRepository>();

            services.AddBroker(configuration);
            return services;
        }
    }
}