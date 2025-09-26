using Birthsys.Identity.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Birthsys.Identity.Infrastructure.Database
{
    public sealed class Context(
        DbContextOptions<Context> options
    ) : DbContext(options), IUnitOfWork
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}