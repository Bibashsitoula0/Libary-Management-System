using BookHive.Dal.AccountRepository;
using BookHive.Dal.DapperConfigure;
using BookHive.Models;
using BookiHive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHive.Dal.StudentRepository
{

    public class StudentRepository : DALConfig, IStudentRepository
    {
        public readonly IDataAccessLayer _dah;
        public StudentRepository(IDataAccessLayer dah)
        {
            _dah = dah;

        }

        public async Task<List<CartViewModel>> getBorrowBookList()
        {
            string query = @"exec get_borrow_book_list";
            var user = await _dah.FetchDerivedModelAsync<CartViewModel>(query);
            return user;
        }

        public async Task<List<CartBookViewModel>> GetListOfApprovcal()
        {
            string query = @"exec get_cart_approval_list";
            var user = await _dah.FetchDerivedModelAsync<CartBookViewModel>(query);
            return user;
        }

        public async Task<List<object>> GetNewStudent()
        {
            string query = @"exec get_total_student_user";            
            var user = await _dah.FetchDerivedModelAsync<object>(query);
            return user;
        }

        public async Task<List<UserDetails>> GetStudentDetails(string userid)
        {
            string query = @"exec sp_get_studentdetail_list @UserId";
            var parameter = new { UserId = userid };
            var user = await _dah.FetchDerivedModelAsync<UserDetails>(query, parameter);
            return user;
        }

        public async Task<List<Book>> GEtTotalBook()
        {
            string query = @"select * from books";   
            var user = await _dah.FetchDerivedModelAsync<Book>(query);
            return user;
        }
    }

}
