using System.Linq;
using System.Xml.Linq;

namespace MetricServer
{
    public static class MetricParser
    {
        public static Metric ParseTests(string xunitReportPath)
        {
            var document = XDocument.Load(xunitReportPath);
            var collections = from collection in document.Descendants("collection")
                              select collection.Attribute("total").Value;

            int value = 0;
            foreach (var total in collections)
            {
                value += int.Parse(total);
            }

            return new Metric { Name = "tests", Value = value.ToString(), Color = value > 0 ? "green" : "red" };
        }

        public static Metric ParseMaintainability(string vsmetricReportPath)
        {
            var document = XDocument.Load(vsmetricReportPath);
            var metricValue = (from m in document.Descendants("Metric")
                               where m.Attribute("Name").Value == "MaintainabilityIndex"
                               select m.Attribute("Value").Value).First();

            int metric = int.Parse(metricValue);

            string color;
            if (metric >= 80)
            {
                color = "green";
            }
            else if (metric >= 40)
            {
                color = "orange";
            }
            else
            {
                color = "red";
            }

            return new Metric { Name = "maintainability", Value = metric.ToString(), Color = color };
        }
    }
}
