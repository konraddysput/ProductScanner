using Microsoft.Extensions.DependencyInjection;
using ProductScanner.Services.Interfaces;
using ProductScanner.Services.Services;

namespace ProductScanner.Api.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IPhotoService, PhotoService>();
            return services;
        }
    }
}
