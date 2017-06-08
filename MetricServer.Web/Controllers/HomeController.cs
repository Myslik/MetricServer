using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MetricServer.Web.Models;
using MetricServer.Web.ViewModels;

namespace MetricServer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly MetricServerContext context;

        public HomeController(MetricServerContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var repositories = (from repository in context.Repository
                               select repository).ToList();

            var repositoryViewModels = new List<RepositoryViewModel>();

            foreach (var repository in repositories)
            {
                var repositoryViewModel = new RepositoryViewModel { Name = repository.Name };

                var metrics = (from metric in context.Metric
                              join measurement in context.Measurement on metric.Id equals measurement.MetricId
                              where measurement.RepositoryId == repository.Id
                              select metric.Name).Distinct().ToList();

                foreach (var metric in metrics)
                {
                    repositoryViewModel.Badges.Add(new BadgeViewModel
                    {
                        Name = metric,
                        Url = Url.Action("Index", "Badge", new { repositoryName = repository.Name, metricName = metric })
                    });
                }

                repositoryViewModels.Add(repositoryViewModel);
            }

            return View(repositoryViewModels);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
