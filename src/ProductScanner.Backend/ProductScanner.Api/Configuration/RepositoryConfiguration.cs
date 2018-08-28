using Microsoft.Extensions.DependencyInjection;
using ProductScanner.Database.Entities;
using ProductScanner.Database.Repository;

namespace ProductScanner.Api.Configuration
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Photo>, Repository<Photo>>();
            services.AddScoped<IRepository<PhotoObject>, Repository<PhotoObject>>();
            return services;
        }
    }
}
