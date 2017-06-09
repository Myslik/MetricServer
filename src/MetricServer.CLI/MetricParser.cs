using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

        public static Metric ParseWarnings(string msbuildlogPath)
        {
            var pattern = new Regex("(\\d+) Warning\\(s\\)");
            var log = File.ReadAllText(msbuildlogPath);
            var match = pattern.Match(log);
            int warnings = 0;
            if (match.Success)
            {
                warnings = int.Parse(match.Groups[1].Value);
            }
            return new Metric { Name = "warnings", Value = warnings.ToString(), Color = warnings > 0 ? "orange" : "green" };
        }
    }
}
