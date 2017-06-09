using Microsoft.EntityFrameworkCore;

namespace MetricServer.Web.Models
{
    public partial class MetricServerContext : DbContext
    {
        public MetricServerContext(DbContextOptions options) : base(options)
        {
        }
    }
}
