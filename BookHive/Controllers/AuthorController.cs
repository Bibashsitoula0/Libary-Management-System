using Bookhive.Service.AuthorService;
using BookHive.Dal.AuthorRepository;
using BookHive.Models;
using BookiHive.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Azure;

namespace BookHive.Controllers
{
    [Authorize(Roles = "admin")]
    public class AuthorController : Controller
    {
        public readonly IAuthorService _authorService;
        public readonly IAuthorRepository _authorrepo;
        private readonly string[] permittedExtensions = { ".jpg", ".jpeg", ".png", ".jpg" };

        public AuthorController(IAuthorService authorService, IAuthorRepository authorrepo)
        {
            _authorService = authorService;
            _authorrepo = authorrepo;
                
        }

        [HttpGet]
        public async Task<object> Index()
        {
            var data = await _authorService.GetList();
            return View(data);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]

        public IActionResult AddAuthor()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]

        public IActionResult UpdateAuthor()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<Object> AddAuthor(AuthorList user)
        {
            try
            {
               var data = await _authorService.AddAuthor(user);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.MessageWarning = "An error occurred while processing your request.";
                return View();
            }

        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<Object> Delete(int Id)
        {
            try
            {               
                var data = _authorService.DeleteUser(Id);
                return RedirectToAction("Index", "Author");
            }
            catch (Exception ex)
            {
                ViewBag.MessageWarning = "An error occurred while processing your request.";
                return View();
            }



        }

        [HttpGet]
     
        public async Task<object> BookList()
        {
            var data = await _authorService.BookList();
            foreach (var item in data)
            {

                string imagePath = "/Image/" + item.bookimage;
                item.bookimage = imagePath;
            }
            return View(data);
        }

        [HttpGet]
        public async Task<object> UpdateBook(int id)
        {
            var authorname = await _authorrepo.GetAuthor();
            var faculty = await _authorrepo.GetFaculty();
            var semister = await _authorrepo.GetSemister();
            var year = await _authorrepo.GetYear();

            ViewBag.Authorname = authorname;
            ViewBag.Faculty = faculty;
            ViewBag.Semister = semister;
            ViewBag.Year = year;

            var data = await _authorrepo.GetBook(id);
           var query= data.FirstOrDefault();
            return View(query);
        }

        [HttpPost]
        public async Task<object> UpdateBook(BookVm books)
        {
            dynamic filename = null;
             if (books.bookimage != null)
            {
                filename = await UploadImage(books.bookimage, books.bookname[0].ToString());
            }

            var book = new Book();
            book.id = books.Id;
            book.bookimage = filename;
            book.bookname = books.bookname;
            book.totalbook = books.totalbook;
            book.publisher = books.publisher;
            book.price = books.price;
            book.authorid = books.authorid;
            book.faculty = books.faculty;
            book.semister = books.semister;
            book.year = books.year;
            book.createdby = "Bibash";
            book.updatedby = "Bibash";
            book.isdeleted = false;

             await _authorrepo.UpdateBook(book);

            return RedirectToAction("BookList");
        }


        [HttpGet]
        public async Task<object> AddBook()
        {
            var authorname = await _authorrepo.GetAuthor();
            var faculty = await _authorrepo.GetFaculty();
            var semister = await _authorrepo.GetSemister();
            var year = await _authorrepo.GetYear();

            ViewBag.Authorname = authorname;
            ViewBag.Faculty = faculty;
            ViewBag.Semister = semister;
            ViewBag.Year = year;

            return View();
        }

        [HttpPost]
        public async Task<object> AddBook(BookVm books)
        {
            dynamic filename=null;
            if (books.bookimage != null)
            {
                 filename = await UploadImage(books.bookimage, books.bookname[0].ToString());
            }
          
            var book= new Book();    
            book.bookimage= filename;
            book.bookname = books.bookname;
            book.totalbook = books.totalbook;
            book.publisher= books.publisher;
            book.price= books.price;
            book.authorid = books.authorid;
            book.faculty=books.faculty;
            book.semister=books.semister;
            book.year= books.year;
            book.createdby = "Bibash";
            book.updatedby = "Bibash";
            book.isdeleted = false;

            await _authorrepo.AddBook(book);

            return RedirectToAction("BookList");
        }

        private async Task<string> UploadImage(IFormFile file, string document_name)
        {

            //string currentdirectory = Path.Combine(Directory.GetCurrentDirectory(), "Image", file.FileName);
            string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "Image");
            string filename = document_name;
            var filenamewithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
            var extension = Path.GetExtension(file.FileName);
            if (FileValid(file, extension))
            {
                filename = filenamewithoutExtension + "-" + Guid.NewGuid();
                var imgfullpath = filename + extension;
                string path = Path.Combine(imgPath, imgfullpath);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                document_name = imgfullpath;

            }
            return document_name;
        }
        private bool FileValid(IFormFile file, string extension)
        {
            if (permittedExtensions.Contains(extension.ToLower()))
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public async Task<Object> DeleteBook(int Id)
        {
            try
            {
                var data = _authorrepo.DeleteBook(Id);
                return RedirectToAction("BookList", "Author");
            }
            catch (Exception ex)
            {
                ViewBag.MessageWarning = "An error occurred while processing your request.";
                return View();
            }



        }
    }
}
