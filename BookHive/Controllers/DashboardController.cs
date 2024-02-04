using BookHive.Dal.StudentRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookHive.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        public DashboardController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [Authorize(Roles = "admin")]
        public async Task<object> Index(string? message)
        {
            ViewBag.Message = message;
            var data= await _studentRepository.GEtTotalBook();
            var totalbook = data.Count();

            ViewBag.Totalbook = totalbook;

            var new_st = await _studentRepository.GetNewStudent();
            var newstudent = new_st.Count();
            ViewBag.newstudent = newstudent;

            var lendbook = await _studentRepository.getBorrowBookList();
            var query = lendbook.Where(x => x.IsTaken == true).ToList();
            var totallend = query.Count();
            ViewBag.totallend = totallend;

            return View();
        }
    }
}
