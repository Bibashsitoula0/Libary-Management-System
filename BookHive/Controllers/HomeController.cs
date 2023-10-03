using Bookhive.Service.AccountService;
using BookHive.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookHive.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAccountService _accountService;

        public HomeController(ILogger<HomeController> logger,IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login", "Account");
        }
        public async Task<IActionResult> Chat()
        {
            var UserList = await _accountService.RegisterGeneralUserList();
            return View(UserList);
          
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}