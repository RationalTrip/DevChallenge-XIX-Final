using Microsoft.Extensions.DependencyInjection;
using Miners.Application.LogicUnits;
using Miners.Application.Services;
using Miners.Infrastructure.LogicUnits;
using Miners.Infrastructure.Services;

namespace TrustNetwork.Infrastructure
{
    public static class ConfigureInfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddLogicUnits();

            services.AddServices();

            return services;
        }

        private static IServiceCollection AddLogicUnits(this IServiceCollection services)
        {
            services.AddScoped<IMineScannerLogicUnit, MineScannerLogicUnit>();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMineScannerService, MineScannerService>();

            return services;
        }
    }
}
