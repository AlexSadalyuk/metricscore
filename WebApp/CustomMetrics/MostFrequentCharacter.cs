using MetricsCore.Interfaces;
using MetricsCore.Models;
using System.IO;
using System.Linq;

namespace WebApp.CustomMetrics
{
    public class MostFrequentCharacter : IMetric //can be also inherited from MetricBase
    {
        private const string MetricName = "The Most Frequetn Character";
        private const string Description = "Shows the most frequent character and its occurrence number";

        public MetricsEntryResult CheckMetric(string text)
        {
            var character = text
                .GroupBy(x => x)
                .Where(x => !string.IsNullOrWhiteSpace(x.Key.ToString()))
                .Select(x => new { key = x.Key, value = x.Count() })
                .OrderByDescending(x => x.value)
                .First();

            MetricsEntryResult metricsEntryResult = new MetricsEntryResult(MetricName, Description)
            {
                Value = character == null ? "String migth be empty" : 
                    $"The most frequent character is '{character.key}' and number of its occurences is {character.value}"
            };

            return metricsEntryResult;
        }
    }
}
