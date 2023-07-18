using BookHive.Helpers;
using BookHive.Models;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookHive.Controllers
{
    public class AccountController : Controller
    {
        private IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly Email _email;

        public AccountController(SignInManager<IdentityUser> signInManager, IConfiguration configuration,Email email)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _email = email;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
         public IActionResult Login()
        {
            return View();     
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
         
            var data  = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
           
          if (data.Count == 0)
            {
                var result = await _signInManager.PasswordSignInAsync(vm.email, vm.password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    ViewBag.Message = "Login Sucessfully";
                    return LocalRedirect("/dashboard");
                }
                else
                {
                    ViewBag.MessageWarning = "Email and Password doesnot match.";
                    return View();
                }
            }
            else
            {
                return LocalRedirect("/dashboard");
            }

        }



        public async Task<ActionResult> OtpVerify(int num)
        {
            if (num == 1)
            {
               var  message= "OTP sent to your email";
                ViewBag.Message = message;
            }           
           
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> OtpVerify(OptVm vm)
        {           
            return View();
        }


        public async Task<ActionResult> ForgetPasswordReset()
        {


            return View();
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgetPassword(LoginVm vm)
        {
            try
            {
                var otpcode = new Random().Next(10000, 99999);
              
                var mail = _email.SendEmail(" ", vm.email, "Reset Password for Libary Management System ", "Your Otp code is " + otpcode + ". Please verify within 3 minutes");
              
                if (mail)
                {
                  
                    return RedirectToAction("OtpVerify", "Account", new { message = 1 });
                }

                return View();           
             
            }
            catch (Exception ex)
            {
            
                ViewBag.ErrorMessage = "An error occurred while sending the OTP: " + ex.Message;
                return View("Error");
            }

        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            return View();
        }



    }
}
