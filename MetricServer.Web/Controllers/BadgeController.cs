using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MetricServer.Web.Controllers
{
    public class BadgeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return Redirect("https://img.shields.io/badge/tests-1-green.svg");
        }
    }
}
