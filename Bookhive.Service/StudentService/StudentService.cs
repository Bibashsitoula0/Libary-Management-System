using BookHive.Dal.StudentRepository;
using BookiHive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookhive.Service.StudentService
{
    public class StudentService : IStudentService
    {
        public readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<List<UserDetails>> GetStudentDetails(string userid)
        {
           var data = await _studentRepository.GetStudentDetails(userid);
            return data;

        }
    }
}
