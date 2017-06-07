using MetricServer.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MetricServer.Web.Controllers
{
    public class BadgeController : Controller
    {
        private readonly MetricServerContext context;

        public BadgeController(MetricServerContext context)
        {
            this.context = context;
        }

        [Route("~/badges/{metricName}")]
        public IActionResult Index(string metricName)
        {
            var badge = (from metric in context.Metric
                        join measurement in context.Measurement on metric.Id equals measurement.MetricId
                        where metric.Name == metricName
                        orderby measurement.Id descending
                        select new { Name = metric.Name, Value = measurement.Value, Color = measurement.Color }).First();
            
            return Redirect($"https://img.shields.io/badge/{badge.Name}-{badge.Value}-{badge.Color}.svg");
        }
    }
}
