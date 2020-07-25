using MetricsCore.Models;

namespace MetricsCore.Interfaces
{
    public interface IMetricsBuilder
    {
        IMetricsBuilder AddMetrics(MetricCaller caller);
        IMetricsBuilder AddMetrics<T>(string name) where T : IMetric;
    }
}