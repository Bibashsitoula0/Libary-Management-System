using Bookhive.Service.AccountService;
using BookHive.Helpers;
using BookHive.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGeneration;
using System.Web.Http;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using System;
using System.IO;
using System.Drawing;
using QRCoder;
using iTextSharp.text.pdf.qrcode;
using System.Drawing.Imaging;
using QRCode = QRCoder.QRCode;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using static iTextSharp.text.pdf.AcroFields;
using BookHive.Dal.AccountRepository;
using AspNetCore;
using BookHive.Dal.StudentRepository;
using BookiHive.Model;
using static Slapper.AutoMapper;

namespace BookHive.Controllers
{
    public class AccountController : Controller
    {
        private IConfiguration _configuration;
        private readonly IStudentRepository _studentRepository;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
        private UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly Email _email;
        private RoleManager<IdentityRole> _roleManager;
        private IAccountService _accountService;
        private IAccountRepository _accountRepository;
        public readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IStudentRepository studentRepository,IAccountRepository accountRepository,SignInManager<ApplicationUser> signInManager, IConfiguration configuration, Email email, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IAccountService accountService, IHttpContextAccessor httpContextAccessor, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _studentRepository = studentRepository;
            _email = email;
            _userManager = userManager;
            _roleManager = roleManager;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            Environment = environment;
            _accountRepository= accountRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string? message)
        {
            ViewBag.Message=message;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            try
            {
            var data = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (data.Count == 0)
            {
                var result = await _signInManager.PasswordSignInAsync(vm.email, vm.password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var isActiveUser = await _accountService.GetCheckActiveUser(vm.email);
                    if (!isActiveUser)
                    {
                        ViewBag.MessageWarning = "User doesnot create account";
                        return View();

                    }

                    var userRoles = await _accountService.Getrole(vm.email);
                        var da = userRoles.FirstOrDefault();


                    if (da.role == "admin")
                    {
                            ViewBag.Message = "Login Sucessfully";
                            return RedirectToAction("Index", "Dashboard", new { message = ViewBag.Message });                           
                       
                    }
                    else if (da.role == "Student")
                    {
                        ViewBag.Message = "Login Sucessfully";
                        return RedirectToAction("Index","Student",new {message= ViewBag.Message });
                    }
                                       
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

                ViewBag.MessageWarning = "An error occurred while processing your request.";
                return View();
            }
            catch (Exception ex)
            {

                ViewBag.MessageWarning = "An error occurred while processing your request.";
                return View();
            }

        }


        public async Task<ActionResult> OtpVerify(int num)
        {
            if (num == 1)
            {
                var message = "OTP sent to your email";
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

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<Object> UserList()
        {
            var UserList = await _accountService.RegisterGeneralUserList();
            return View(UserList);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<Object> Register(User user)
        {
            try
            {
                var applicationUser = new ApplicationUser()
                {
                    UserName = user.email,
                    Email = user.email,
                    fullName = user.username,
                    is_active = true,
                    schoolCode="0000",
                    filename="aaaaaa"
                };

                var checkuserexist = await _accountService.Getrole(user.email);                
                if (checkuserexist.Count == 0)
                {
                    var result = await _userManager.CreateAsync(applicationUser, user.confirmpassword);
                    if (result.Succeeded)
                    {

                        var role = "admin";
                        if (!await _roleManager.RoleExistsAsync(role))
                            await _roleManager.CreateAsync(new IdentityRole(role));

                        await _userManager.AddToRoleAsync(applicationUser, role);

                        return LocalRedirect("/Account/UserList");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            var errorMessage = error.Description;
                            ViewBag.MessageWarning = errorMessage;
                            return View();
                        }
                    }
                }
                else
                {
                    ViewBag.MessageWarning = "Email already exists...plz enter the unique email";
                    return View();

                }



                

                return View();

            }
            catch (Exception ex)
            {
                ViewBag.MessageWarning = "An error occurred while processing your request.";
                return View();
            }



        }

        [HttpGet]
        public async Task<Object> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<Object> Delete(string Id)
        {
            var data = _accountService.DeleteUser(Id);

            return RedirectToAction("UserList", "Account");
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<Object> StudentDelete(string Id)
        {
            var data = _accountService.DeleteUser(Id);

            return RedirectToAction("StudentUserList", "Account");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<Object> StudentUserList()
        {
            var UserList = await _accountService.StudentUserList();
            foreach (var item in UserList)
            {
                
                string imagePath =  "/Image/" + item.filename;
                item.filename = imagePath;
            }
            
            return View(UserList);
        }


        [Authorize(Roles = "Student")]
        [HttpGet]
        public string GenerateQr(string codeId, string emails)
        {
            try
            {
                dynamic fileName = null;
                string qrCodePath = Path.Combine(Directory.GetCurrentDirectory(), "Image");
                using (MemoryStream ms = new MemoryStream())
                {
                    var Code = codeId;
                    var Userid = emails;

                    var url = _configuration["ServerUrl:url"];
                    var urlpath = $"{url}?code={Code}&userEmail={Userid}";

                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(urlpath, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);

                    using (Bitmap bitmap = qrCode.GetGraphic(60))
                    {
                         fileName = $"{Code}_QRCode.png"; //  file name is email + qr name
                        string filePath = Path.Combine(qrCodePath, fileName);
                        bitmap.Save(filePath, ImageFormat.Png);
                    }
                }

                return fileName;
            }
            catch (Exception ex )
            {

                return ex.Message;
            }



        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> RegisterStudent()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterStudent(LoginVm vm)
        {
            var email =vm.email;
            var code=vm.code;
            var username = vm.username;

            var checkuserexist = await _accountService.Getrole(email);
            if (checkuserexist.Count == 0)
            {
                var session = _httpContextAccessor.HttpContext.Session;
                session.SetString("Email", email);
                session.SetString("Code", code);
                session.SetString("Username", username);

                return RedirectToAction("StudentSendOtp", "Account");

            }
            else
            {
                ViewBag.MessageWarning = "Email already exists...Plz enter the unique email";
                return View();

            }

        }

        [HttpGet]
        public async Task<IActionResult> StudentSendOtp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StudentSendOtp(OptVm vm)
        {
            try
            {
                var email = HttpContext.Session.GetString("Email");
                var otpcode = new Random().Next(10000, 99999);                

                var mail = _email.SendEmail(" ", email, " Verify Email for Libary Management System ", "Your Otp code is " + otpcode + ". Please verify within 3 minutes");                

                if (mail)
                {
                    var session = _httpContextAccessor.HttpContext.Session;
                    session.SetString("VerifyCode", otpcode.ToString());

                    return RedirectToAction("StudentVerify", "Account", new { message = 1 });
                }
                else
                {
                    ViewBag.ErrorMessage = "Error Occur due to sending mail";
                    return View();
                }               

            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = "An error occurred while sending the OTP: " + ex.Message;
                return View("Error");
            }           
        }


        [HttpGet]
        public async Task<IActionResult> StudentVerify(int message)
        {
            if (message == 1)
            {
                var messages = "OTP sent to your email";
                ViewBag.Message = messages;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StudentVerify(OptVm vm)
        {
            try

            {
            var verficode = HttpContext.Session.GetString("VerifyCode");
            var sendotp = vm.otp;   

            if (verficode == sendotp)
            {
                return RedirectToAction("StudentConfirmPassword", "Account");
            }
            else
            {
                ViewBag.ErrorMessage = "Otp code doesnot match";
                return View();
            }
            }
            catch (Exception)
            {

                ViewBag.ErrorMessage = "Error occur due to verify otp";
                return View();
            }
        }


        [HttpGet]
        public async Task<IActionResult> StudentConfirmPassword()
        {
           
         return View();
        
        }

        [HttpPost]
        public async Task<IActionResult> StudentConfirmPassword(User urs)
        {
            var emails = HttpContext.Session.GetString("Email");
            var codes = HttpContext.Session.GetString("Code");
            var usernames = HttpContext.Session.GetString("Username");            
            var password = urs.confirmpassword;

            var filenames = GenerateQr(codes, emails);

            var applicationUser = new ApplicationUser()
            {
                UserName = emails,
                Email = emails,
                fullName = usernames,
                is_active = true,
                schoolCode= codes,
                filename= filenames

            };

            var result = await _userManager.CreateAsync(applicationUser, urs.confirmpassword);
            if (result.Succeeded)
            {

                var role = "Student";
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));

                await _userManager.AddToRoleAsync(applicationUser, role);
                            

                HttpContext.Session.Remove("Email");
                HttpContext.Session.Remove("Code");
                HttpContext.Session.Remove("Username");
                HttpContext.Session.Remove("VerifyCode");

                ViewBag.Message = "User Created sucessfully..Now you can login";
                return RedirectToAction("Login","Account" ,new {message = ViewBag.Message });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    var errorMessage = error.Description;
                    ViewBag.ErrorMessage = errorMessage;
                    return View();
                }
            }


            return View();

        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> StudentDetails(string Id)
        {
            var data = await _accountService.Getstudentbyid(Id);
            var details = data.FirstOrDefault();
            details.filename = "/Image/" + details.filename;
            return View(details);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<object> BookHistory(string code,string userEmail)
        {
              StudentData obj = new StudentData();
            var data = await _userManager.FindByEmailAsync(userEmail);
            if (data == null)
            {
                return RedirectToAction("Login", "Account");

            }
            else
            {
                var query = await _studentRepository.getBorrowBookList();
                var op = query.Where(x => x.UserId == data.Id && x.IsTaken==true).ToList();
                obj.cartViewModels = op;                
                var value = await _accountService.Getstudentbyid(data.Id);
                obj.userdata= value;
            }
                     
            return View(obj);
        }





    }
}
