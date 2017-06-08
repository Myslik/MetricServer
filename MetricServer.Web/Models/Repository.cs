using System;
using System.Collections.Generic;

namespace MetricServer.Web.Models
{
    public partial class Repository
    {
        public Repository()
        {
            Measurement = new HashSet<Measurement>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Measurement> Measurement { get; set; }
    }
}
