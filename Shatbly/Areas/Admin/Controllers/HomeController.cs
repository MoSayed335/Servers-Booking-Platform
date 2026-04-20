using Microsoft.AspNetCore.Mvc;

namespace Shatbly.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
