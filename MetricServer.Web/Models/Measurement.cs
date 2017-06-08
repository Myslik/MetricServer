using System;
using System.Collections.Generic;

namespace MetricServer.Web.Models
{
    public partial class Measurement
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Color { get; set; }
        public int MetricId { get; set; }
        public DateTime TakenDate { get; set; }
        public int RepositoryId { get; set; }

        public virtual Metric Metric { get; set; }
        public virtual Repository Repository { get; set; }
    }
}
