using Microsoft.AspNetCore.Mvc;

namespace WebApplicationMaxim.Areas.Manage.Controllers
{
    public class DashboardController : Controller
    {

        [Area("Manage")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
