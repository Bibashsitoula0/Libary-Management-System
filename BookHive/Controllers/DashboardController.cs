using Microsoft.AspNetCore.Mvc;

namespace BookHive.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
