using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricServer.Web.Controllers
{
    public class PublishController : Controller
    {
        [HttpPost]
        public IActionResult Index(string project, string branch, IFormFile file)
        {
            return View();
        }
    }
}
