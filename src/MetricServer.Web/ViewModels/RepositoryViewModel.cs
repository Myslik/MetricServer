using System.Collections.Generic;

namespace MetricServer.Web.ViewModels
{
    public class RepositoryViewModel
    {
        public RepositoryViewModel()
        {
            Badges = new List<BadgeViewModel>();
        }

        public string Name { get; set; }
        public IList<BadgeViewModel> Badges { get; set; }
    }
}
