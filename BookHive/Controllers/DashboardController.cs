using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookHive.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize(Roles = "admin")]
        public IActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View();
        }
    }
}
