namespace MetricsCore.Models
{
    public class MetricsEntryResult
    {
        public string Name { get; }
        public string Description { get; }
        public object Value { get; set; }

        public MetricsEntryResult(string name, string description, object value = null)
        {
            Name = name ?? "Unknown metric";
            Description = description ?? "No description provided";
            Value = value;
        }
    }
}