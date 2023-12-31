﻿using BookHive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookhive.Service.AccountService
{
    public interface IAccountService
    {
        Task<List<RegisterList>> RegisterGeneralUserList(); 
        Task<List<RegisterList>> StudentUserList();
        Task<bool> DeleteUser(string Id);
        Task<bool> GetCheckActiveUser(string user_id); 
       Task<List<RegisterList>> Getrole(string email); 
       Task<List<RegisterList>> Getstudentbyid(string userid);
    }
}
