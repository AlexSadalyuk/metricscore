using MetricsCore.Concrete;
using MetricsCore.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MetricsCore.Extensions
{
    public static class MetricsServiceCollectionExtensions
    {
        public static IMetricsBuilder AddMetricsContainer(this IServiceCollection services)
        {
            services.AddSingleton<IMetricsService, MetricsService>();
            return new MetricsBuilder(services);
        }
    }
}