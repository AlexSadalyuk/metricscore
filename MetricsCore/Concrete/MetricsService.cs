using MetricsCore.Interfaces;
using MetricsCore.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MetricsCore.Concrete
{
    internal class MetricsService : IMetricsService
    {

        private readonly IOptions<MetricsServiceOptions> _options;
        private readonly IServiceScopeFactory _scopeFactory;

        public MetricsService(IOptions<MetricsServiceOptions> options, IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory), "Scope factory cannot be null");
            _options = options ?? throw new ArgumentNullException(nameof(options), "Options cannot be null");

            ValidateOptions(options.Value.Callers);
        }

        public IEnumerable<MetricsEntryResult> RunMetricsCheck(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text), "Text cannot be null");
            }

            using IServiceScope scope = _scopeFactory.CreateScope();

            return _options.Value.Callers
                .Select(mc => RunCheck(scope, mc, text))
                .ToList();
        }

        public MetricsEntryResult RunMetricsCheck(string text, string metricName)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text), "Text cannot be null");
            }

            if (metricName == null)
            {
                throw new ArgumentNullException(nameof(metricName), "Metric name cannot be null");
            }

            MetricCaller mc = _options.Value.Callers.Single(metric => metric.Name == metricName);

            if (mc == null)
            {
                throw new ArgumentNullException(nameof(metricName), $"There is no metric with such name: {metricName}");
            }

            MetricsEntryResult metricsEntryResult = null;

            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                metricsEntryResult = RunCheck(scope, mc, text);
            }

            if (metricsEntryResult == null)
            {
                throw new ArgumentNullException(nameof(metricsEntryResult), "Metric must return MetricEntryResult");
            }

            return metricsEntryResult;
        }

        public IEnumerable<MetricsEntryResult> RunMetricsCheck(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), "Text cannot be null");
            }

            using StreamReader sr = new StreamReader(stream);
            return RunMetricsCheck(sr.ReadToEnd());
        }

        public MetricsEntryResult RunMetricsCheck(Stream stream, string metricName)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream),"Stream cannot be null");
            }

            if (metricName == null)
            {
                throw new ArgumentNullException(nameof(metricName), "Metric name cannot be null");
            }

            using StreamReader sr = new StreamReader(stream);
            return RunMetricsCheck(sr.ReadToEnd(), metricName);
        }

        #region private methods

        private MetricsEntryResult RunCheck(IServiceScope scope, MetricCaller caller, string text)
        {
            IMetric metric = caller.Factory(scope.ServiceProvider);

            if (text == null)
            {
                throw new ArgumentNullException($"{nameof(text)}", "Stream argument should be provided");
            }

            MetricsEntryResult result = metric.CheckMetric(text);

            return result;
        }

        private void ValidateOptions(IEnumerable<MetricCaller> callers)
        {
            List<string> duplicateNames = callers
                .GroupBy(r => r.Name, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateNames.Count > 0)
            {
                throw new ArgumentException($"Duplicates found: {string.Join(",", duplicateNames)}", nameof(callers));
            }
        }
        #endregion private methods
    }
}