using Microsoft.AspNetCore.Mvc;

namespace BookHive.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> OtpVerify()
        {          

            return View();
        }
        public async Task<ActionResult> ForgetPasswordReset()
        {


            return View();
        }

    }
}
