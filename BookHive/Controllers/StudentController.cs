using Bookhive.Service.AccountService;
using Bookhive.Service.AuthorService;
using Bookhive.Service.CurrentUserService;
using Bookhive.Service.StudentService;
using BookHive.Dal.AuthorRepository;
using BookHive.Helpers;
using BookHive.Models;
using BookiHive.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookHive.Controllers
{
    public class StudentController : Controller
    {
        public readonly IStudentService _studentService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorService _authorService;
        private readonly IAuthorRepository _authorrepo;
        public readonly ICurrentUserService _currentUserService;
        public StudentController(IStudentService studentService, UserManager<ApplicationUser> userManager, IAuthorService authorService, ICurrentUserService currentUserService, IAuthorRepository authorrepo)
        {
            _studentService = studentService;
            _userManager = userManager;
            _authorService = authorService;
           _currentUserService = currentUserService;
            _authorrepo= authorrepo;    
        }

        public async Task<IActionResult> Index( string message)
        {
            ViewBag.Message = message;
             UserDetails app = new UserDetails();
            var user = await  _userManager.GetUserAsync(User);
            app.phonenumber=user.PhoneNumber;
            app.email=user.Email;
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
            if (data.image !=null)
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
            
            model.time=DateTime.Now;           
            var user = await _userManager.GetUserAsync(User);
            model.userid = user.Id;

            var cart = new Cart();
            cart.userid = user.Id;
            cart.time = DateTime.Now;
            cart.bookid= model.bookid;
            cart.bookname = model.bookname;
            

            var data = await _authorrepo.SaveCart(cart);




            var data1 = await _authorService.BookList();
            foreach (var item in data1)
            {
                string imagePath = "/Image/" + item.bookimage;
                item.bookimage = imagePath;
            }

            var carts = await _authorrepo.getCart(model.userid);            
            var count=carts.Count();
            foreach (var item in carts)
            {
                item.totalcount_distinct = count ;
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
            var userid=user.Id;

            var carts = await _authorrepo.getCart(userid);

            var cart = await _authorrepo.getCart(user.Id);
            var count = cart.Count();
            foreach (var item in cart)
            {
                item.totalcount_distinct = count;
            }

            ViewBag.Cart = cart;


            return View (carts);
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

        }
}
