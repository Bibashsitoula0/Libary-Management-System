using Bookhive.Service.AccountService;
using Bookhive.Service.AuthorService;
using Bookhive.Service.CurrentUserService;
using Bookhive.Service.StudentService;
using BookHive.Dal.AuthorRepository;
using BookHive.Dal.StudentRepository;
using BookHive.Helpers;
using BookHive.Models;
using BookiHive.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static iTextSharp.text.pdf.AcroFields;
using static QRCoder.PayloadGenerator;

namespace BookHive.Controllers
{
    public class StudentController : Controller
    {
        public readonly IStudentService _studentService;
        public readonly IStudentRepository _studentRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorService _authorService;
        private readonly Email _email;
        private readonly IAuthorRepository _authorrepo;
        public readonly ICurrentUserService _currentUserService;
        public StudentController(Email email,IStudentRepository studentRepository,IStudentService studentService, UserManager<ApplicationUser> userManager, IAuthorService authorService, ICurrentUserService currentUserService, IAuthorRepository authorrepo)
        {
            _email = email;
            _studentService = studentService;
            _userManager = userManager;
            _authorService = authorService;
            _currentUserService = currentUserService;
            _authorrepo = authorrepo;
            _studentRepository= studentRepository;
        }

        public async Task<IActionResult> Index(string message)
        {
            ViewBag.Message = message;
            UserDetails app = new UserDetails();
            var user = await _userManager.GetUserAsync(User);
            app.phonenumber = user.PhoneNumber;
            app.email = user.Email;
            app.codeno = user.schoolCode;
            app.Qrcode = "/Image/" + user.filename;
            app.userid = user.Id;
            app.full_name = user.fullName;

            var cart = await _authorrepo.getCart(user.Id);
            var count = cart.Count();
            foreach (var item in cart)
            {
                item.totalcount_distinct = count;
            }

            ViewBag.Cart = cart;

            return View(app);
        }


        [HttpGet]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Profile()
        {
            // get login user 
            var user = await _userManager.GetUserAsync(User);

            // get studentby userid
            var datas = await _studentService.GetStudentDetails(user.Id);
            var data = datas.FirstOrDefault();
            data.Qrcode = "/Image/" + data.Qrcode;
            if (data.image != null)
            {
                data.image = "/Image/" + data.image;
            }

            var cart = await _authorrepo.getCart(user.Id);
            var count = cart.Count();
            foreach (var item in cart)
            {
                item.totalcount_distinct = count;
            }

            ViewBag.Cart = cart;
            return View(data);
        }



        [Authorize(Roles = "Student")]
        [HttpGet]
        public async Task<IActionResult> BookList()
        {

            var data = await _authorService.BookList();
            foreach (var item in data)
            {
                string imagePath = "/Image/" + item.bookimage;
                item.bookimage = imagePath;
            }
            var user = await _userManager.GetUserAsync(User);
            var cart = await _authorrepo.getCart(user.Id);
            var count = cart.Count();
            foreach (var item in cart)
            {
                item.totalcount_distinct = count;
            }

            ViewBag.Cart = cart;
            return View(data);
        }


        [Authorize(Roles = "Student")]
        [HttpPost]
        public async Task<IActionResult> BookList(CartVm model)
        {
            dynamic da = model.count;

            model.time = DateTime.Now;
            var user = await _userManager.GetUserAsync(User);
            model.userid = user.Id;

            var cart = new Cart();
            cart.userid = user.Id;
            cart.time = DateTime.Now;
            cart.bookid = model.bookid;
            cart.bookname = model.bookname;

            // data to check the bookid if there is same book count or not 

            var getcartbyid = await _authorrepo.getCartList(model.bookid, model.userid);
            if (getcartbyid.Count > 0)
            {

            }
            else
            {
                var data = await _authorrepo.SaveCart(cart);
            }

            var data1 = await _authorService.BookList();
            foreach (var item in data1)
            {
                string imagePath = "/Image/" + item.bookimage;
                item.bookimage = imagePath;
            }

            var carts = await _authorrepo.getCart(model.userid);
            var count = carts.Count();
            foreach (var item in carts)
            {
                item.totalcount_distinct = count;
                item.userid = user.Id;
            }
            ViewBag.Cart = carts;
            return View(data1);
        }



        [HttpGet]
        [Authorize(Roles = "Student")]
        public async Task<object> CartList()
        {
            var user = await _userManager.GetUserAsync(User);
            var userid = user.Id;

            var carts = await _authorrepo.getCart(userid);

            var cart = await _authorrepo.getCart(user.Id);
            var count = cart.Count();
            foreach (var item in cart)
            {
                item.totalcount_distinct = count;
                
            }
            ViewBag.Cart = cart;
            return View(carts);
        }

        [HttpGet]
        [Authorize(Roles = "Student")]
        public async Task<object> DeleteCart(int Id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userid = user.Id;
                var data = _authorrepo.DeleteCart(Id, userid);
                return RedirectToAction("CartList", "Student");
            }
            catch (Exception ex)
            {
                ViewBag.MessageWarning = "An error occurred while processing your request.";
                return View();
            }

        }

        [HttpPost]
        public async Task<IActionResult> RequestBook(CartVm model)
        {
            var user = await _userManager.GetUserAsync(User);
            model.userid = user.Id;

            foreach (var item in model.cartlist)
            {
                var updateboolforrequest = await _authorrepo.ApproveRequest( model.Request,item.cartid,item.bookid);
            }

            
            return RedirectToAction("CartList", "Student");

        }

        [HttpGet]
        public async Task<IActionResult> RequestList(string? message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.Message = message;

            }
            List<CartBookViewModel> list = new List<CartBookViewModel>();   

           

            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault();

            if (userRole == "Student")
            {
                list = await _studentRepository.GetListOfApprovcal();
                list = list.Where(x => x.userid == user.Id && x.IsApproved == true && x.istaken==false).ToList();
                foreach (var item in list)
                {
                    string imagePath = "/Image/" + item.filename;
                    item.filename = imagePath;
                }

            }
            else{

                list = await _studentRepository.GetListOfApprovcal();
                foreach (var item in list)
                {
                    string imagePath = "/Image/" + item.filename;
                    item.filename = imagePath;
                }

            }
            
            return View(list);

        }


        [HttpGet]
        public async Task<IActionResult> Email(string? email)
        {

            ViewBag.email = email;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(SendEmailVm vm)
        {
            var mail = _email.SendEmail(" ", vm.email, vm.subject,vm.description);
            if (mail)
            {
                return RedirectToAction("RequestList", "Student", new { message = "Email Send Successfully" });
            }
            else 
            {
                return RedirectToAction("RequestList", "Student", new { message = "Email Send Failed" });
            }
        }


        [HttpGet]
        public async Task<IActionResult> ApprovedBook(int cartId, bool checkedValue)
        {
            var updateboolforrequest = await _authorrepo.ApproveStudent(checkedValue, cartId);
            if (updateboolforrequest)
            {
                return RedirectToAction("RequestList", "Student", new { message = "Approved successfully" });
            }
            else
            {
                return RedirectToAction("RequestList", "Student", new { message = "Reject successfully" });
            }
          
        }

        [HttpGet]
        public async Task<IActionResult> IsBookTaken(int cartId, bool checkedValue)
        {
            var updateboolforrequest = await _authorrepo.IsBookTaken(checkedValue, cartId);
            if (updateboolforrequest)
            {
                return RedirectToAction("RequestList", "Student", new { message = "Student took the book successfully" });
            }
            else
            {
                return RedirectToAction("RequestList", "Student", new { message = "Student took the book successfully" });
            }            
        }

        [HttpGet]
        public async Task<IActionResult> Borrow()
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault();


            var data = await _studentRepository.getBorrowBookList();
           var query= data.Where(x=>x.UserId== user.Id && x.IsTaken==true).ToList();
            return View(query);
        }


        [HttpGet]
        public async Task<IActionResult> BorrowList()
        {
            var data = await _studentRepository.getBorrowBookList();
            var query = data.Where(x=>x.IsTaken == true).ToList();
            return View(query);
        }

    }


}
