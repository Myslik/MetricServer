namespace MetricServer
{
    public class Metric
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Color { get; set; }

        public override string ToString()
        {
            return $"Metric: {Name} - {Value} - {Color}";
        }
    }
}
