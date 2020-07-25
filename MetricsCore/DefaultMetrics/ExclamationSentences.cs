using MetricsCore.Interfaces;
using MetricsCore.Models;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace MetricsCore.DefaultMetrics
{
    public class ExclamationSentences : IMetric
    {
        private const string MetricName = "Exclamation Sentences";
        private const string Description = "Shows amount of exclamation sentences";
        private readonly Regex _regex = new Regex(@"([A-z0-9][^!]*[!])|([А-я0-9][^!]*[!])");

        public MetricsEntryResult CheckMetric(string text)
        {
            MatchCollection matches = _regex.Matches(text);

            MetricsEntryResult metricsEntryResult = new MetricsEntryResult(MetricName, Description)
            {
                Value = matches.Count > 0 ? $"Found {matches.Count} exclamation sentences" : "No exclamation sentences found"
            };

            return metricsEntryResult;
        }

    }
}