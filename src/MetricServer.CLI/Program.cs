using NDesk.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace MetricServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string token = string.Empty;
            string repositoryName = string.Empty;
            bool xunitReport = false;
            string xunitReportPath = string.Empty;
            bool vsmetricReport = false;
            string vsmetricReportPath = string.Empty;
            bool msbuildLog = false;
            string msbuildLogPath = string.Empty;
            bool consoleOnly = false;

            var p = new OptionSet()
            {
                { "t|token=", "Authentication token for Metric Server.", v => token = v },
                { "repository=", "Repository name.", v => repositoryName = v },
                { "xunit=", "Report from XUnit.", v => { xunitReportPath = v; xunitReport = true; } },
                { "vsmetrics=", "Report from VS Metrics.", v => { vsmetricReportPath = v; vsmetricReport = true; } },
                { "msbuildlog=", "Log file from MSBuild.", v => { msbuildLogPath = v; msbuildLog = true; } },
                { "c|console", "Write metrics only to console.", v => consoleOnly = v != null }
            };
            var extra = p.Parse(args);

            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(repositoryName))
            {
                Console.WriteLine("Required parameters missing.");
                return;
            }

            var metrics = new List<Metric>();

            if (xunitReport)
            {
                metrics.Add(MetricParser.ParseTests(xunitReportPath));
            }

            if (vsmetricReport)
            {
                metrics.Add(MetricParser.ParseMaintainability(vsmetricReportPath));
            }

            if (msbuildLog)
            {
                metrics.Add(MetricParser.ParseWarnings(msbuildLogPath));
            }

            if (consoleOnly)
            {
                foreach(var metric in metrics)
                {
                    Console.WriteLine(metric.ToString());
                }
                return;
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://metricserverweb.azurewebsites.net");
                foreach (var metric in metrics)
                {
                    var content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        { "metricName", metric.Name },
                        { "metricValue", metric.Value },
                        { "metricColor", metric.Color },
                        { "token", token }
                    });
                    var response = client.PostAsync($"/repositories/{repositoryName}/measurement", content).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Failed to post metrics to server.");
                    }
                    else
                    {
                        Console.WriteLine("Metrics published tp server.");
                    }
                }
            }
        }
    }
}
