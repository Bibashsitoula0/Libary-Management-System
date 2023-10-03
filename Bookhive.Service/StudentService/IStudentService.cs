using BookHive.Models;
using BookiHive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookhive.Service.StudentService
{
    public interface IStudentService
    {
        Task<List<UserDetails>> GetStudentDetails(string userid);
    }
}
