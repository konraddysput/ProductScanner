using Microsoft.Extensions.DependencyInjection;
using ProductScanner.Services.Interfaces;
using ProductScanner.Services.Services;

namespace ProductScanner.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            return services;
        }
    }
}
