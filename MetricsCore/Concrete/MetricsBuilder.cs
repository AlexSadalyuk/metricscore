using MetricsCore.Interfaces;
using MetricsCore.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MetricsCore.Concrete
{
    internal class MetricsBuilder : IMetricsBuilder
    {
        private readonly IServiceCollection _services;

        public MetricsBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public IMetricsBuilder Add(MetricCaller caller)
        {
            if (caller == null)
            {
                throw new ArgumentNullException(nameof(caller), "Caller object cannot be null");
            }

            _services.Configure<MetricsServiceOptions>(options =>
            {
                options.Callers.Add(caller);
            });

            return this;
        }

        public IMetricsBuilder AddMetrics<T>(string name) where T : IMetric
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name), "Name cannot be null");
            }

            MetricCaller mr = new MetricCaller(name, service => ActivatorUtilities.GetServiceOrCreateInstance<T>(service));

            return Add(mr);
        }
    }
}