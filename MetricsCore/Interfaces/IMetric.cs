using MetricsCore.Models;
using System.IO;

namespace MetricsCore.Interfaces
{
    public interface IMetric
    {
        MetricsEntryResult CheckMetric(string text);
    }
}