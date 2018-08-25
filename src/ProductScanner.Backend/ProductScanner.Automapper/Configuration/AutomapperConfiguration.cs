using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ProductScanner.Automapper.Configuration
{
    public static class AutomapperConfiguration
    {
        /// <summary>
        /// To avoid this extension method, you have to pass current assembly (or assembly with profiles) as an argument to AddAtuoMapper method
        /// Use this method to avoid problems with profiles and use profiles defined in diffrent assemby;
        /// </summary>
        public static IServiceCollection AddMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper();
            return services;
        }
    }
}
