using System;
using System.Collections.Generic;

namespace MetricServer.Web.Models
{
    public partial class Metric
    {
        public Metric()
        {
            Measurement = new HashSet<Measurement>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Measurement> Measurement { get; set; }
    }
}
