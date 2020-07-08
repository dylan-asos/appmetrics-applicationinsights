using Microsoft.ApplicationInsights.DataContracts;
using System;

namespace App.Metrics.Reporting.ApplicationInsights
{
    internal static class MetricFactory
    {
        private const string ContextKey = "context";

        internal static string CreateName<T>(MetricValueSourceBase<T> source)
            => source.IsMultidimensional ? source.MultidimensionalName : source.Name;

        internal static MetricTelemetry CreateMetric<T>(
            MetricValueSourceBase<T> source,
            string name,
            string contextName,
            DateTimeOffset now)
        {
            var mt = new MetricTelemetry
            {
                Name = name,
                MetricNamespace = contextName,
                Timestamp = now,
            };

            mt.Properties[ContextKey] = contextName;
            source.Tags.CopyTo(mt);

            return mt;
        }

        internal static MetricTelemetry CreateMetric<T>(
            MetricValueSourceBase<T> source,
            string contextName,
            DateTimeOffset now)
        => CreateMetric(source, CreateName(source), contextName, now);
    }
}
