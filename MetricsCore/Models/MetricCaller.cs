using MetricsCore.Interfaces;
using System;

namespace MetricsCore.Models
{
    public sealed class MetricCaller
    {
        public MetricCaller(string name, Func<IServiceProvider, IMetric> factory)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name must be provided");
            Factory = factory ?? throw new ArgumentNullException(nameof(factory), "Factory hasn't been provided");
        }

        public string Name { get; }
        public Func<IServiceProvider, IMetric> Factory { get; }
    }
}