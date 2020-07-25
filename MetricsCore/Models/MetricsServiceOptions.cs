using System.Collections.Generic;

namespace MetricsCore.Models
{
    public class MetricsServiceOptions
    {
        public List<MetricCaller> Callers { get; }

        public MetricsServiceOptions()
        {
            Callers = new List<MetricCaller>();
        }
    }
}
