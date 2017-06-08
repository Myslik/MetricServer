using MetricServer.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MetricServer.Web.Controllers
{
    public class MeasurementController : Controller
    {
        private readonly MetricServerContext context;

        public MeasurementController(MetricServerContext context)
        {
            this.context = context;
        }

        [Route("~/repositories/{repositoryName}/measurement"), HttpPost]
        public IActionResult Index(string repositoryName, string metricName, string metricValue, string metricColor, string token)
        {
            if (!ValidateToken(token))
            {
                return Unauthorized();
            }

            Repository repository = context.Repository.Where(r => r.Name == repositoryName).SingleOrDefault();
            if (repository == null)
            {
                var trackedRepository = context.Repository.Add(new Repository { Name = repositoryName });
                context.SaveChanges();
                repository = trackedRepository.Entity;
            }

            Metric metric = context.Metric.Where(m => m.Name == metricName).SingleOrDefault();
            if (metric == null)
            {
                var trackedMetric = context.Metric.Add(new Metric { Name = metricName });
                context.SaveChanges();
                metric = trackedMetric.Entity;
            }

            context.Measurement.Add(new Measurement
            {
                RepositoryId = repository.Id,
                MetricId = metric.Id,
                Value = metricValue,
                Color = metricColor
            });

            context.SaveChanges();

            return NoContent();
        }

        private bool ValidateToken(string token)
        {
            using (SHA512 shaM = SHA512.Create())
            {
                var hash = shaM.ComputeHash(Encoding.UTF8.GetBytes(token));
                var hex = new StringBuilder(128);
                foreach(var b in hash)
                {
                    hex.Append(b.ToString("X2"));
                }
                return hex.ToString() == "EAA77DFD2504DECE3E640EF84E6C0455FA0419D32CEBD9E1C4B2C599E807967089E8AF5ADAEB4F125CB4A139678FB38DD0D058FB5E20BD7E83780AE373D78481";
            }
        }
    }
}
