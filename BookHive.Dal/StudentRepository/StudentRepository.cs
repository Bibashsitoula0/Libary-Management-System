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

        public async Task<List<UserDetails>> GetStudentDetails(string userid)
        {
            string query = @"exec sp_get_studentdetail_list @UserId";
            var parameter = new { UserId = userid };
            var user = await _dah.FetchDerivedModelAsync<UserDetails>(query, parameter);
            return user;
        }
    }

}
