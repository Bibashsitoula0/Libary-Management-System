﻿using BookiHive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHive.Dal.StudentRepository
{
    public interface IStudentRepository
    {
        Task<List<UserDetails>> GetStudentDetails(string userid);
    }
}
