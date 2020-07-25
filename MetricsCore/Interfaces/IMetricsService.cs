using MetricsCore.Models;
using System.Collections.Generic;
using System.IO;

namespace MetricsCore.Interfaces
{
    public interface IMetricsService
    {
        IEnumerable<MetricsEntryResult> RunMetricsCheck(string text);
        IEnumerable<MetricsEntryResult> RunMetricsCheck(Stream stream);
        MetricsEntryResult RunMetricsCheck(string text, string metricName = null);
        MetricsEntryResult RunMetricsCheck(Stream text, string metricName = null);
    }
}