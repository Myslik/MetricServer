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

        [Route("~/repositories/{repositoryName}/badges/{metricName}"), HttpGet]
        public IActionResult Index(string repositoryName, string metricName)
        {
            var badge = (from measurement in context.Measurement
                        join metric in context.Metric on measurement.MetricId equals metric.Id
                        join repository in context.Repository on measurement.RepositoryId equals repository.Id
                        where metric.Name == metricName && repository.Name == repositoryName
                        orderby measurement.Id descending
                        select new { Name = metric.Name, Value = measurement.Value, Color = measurement.Color }).FirstOrDefault();

            if (badge == null)
            {
                return Redirect($"https://img.shields.io/badge/{metricName}-unknown-lightgray.svg");
            }
            
            return Redirect($"https://img.shields.io/badge/{badge.Name}-{badge.Value}-{badge.Color}.svg");
        }
    }
}
