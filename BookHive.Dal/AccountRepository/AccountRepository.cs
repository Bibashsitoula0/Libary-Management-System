using BookHive.Dal.DapperConfigure;
using BookHive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHive.Dal.AccountRepository
{
    public class AccountRepository : DALConfig, IAccountRepository
    {
        public readonly IDataAccessLayer _dah;
        public AccountRepository(IDataAccessLayer dah)
        {
            _dah = dah;

        }

        public async Task<bool> DeleteUser(string userId)
        {
            string query = @"update AspNetUsers set 
                        is_active = @isActive              
                        where
                        Id = @id                       
                ";
            var parameters = new
            {
                isActive = false,
                id = userId
            };
            var data = await _dah.FetchDerivedModelAsync<dynamic>(query, parameters);
            return true;
        }

        public async Task<List<RegisterList>> RegisterGeneralUserList()
        {
            string query = @"exec sp_get_user_list";
            var user = await _dah.FetchDerivedModelAsync<RegisterList>(query);
            return user;
        }
        public async Task<List<RegisterList>> StudentUserList()
        {
            string query = @"exec sp_get_user_list_of_student";
            var user = await _dah.FetchDerivedModelAsync<RegisterList>(query);
            return user;
        }


        public async Task<bool> GetCheckActiveUser(string user_id)
        {
            string query = @"select * from ""AspNetUsers"" au                                    
                                    where au.""Email"" = @user_id and au.""is_active"" = @isactive";
            var parameters = new { user_id = user_id,isactive="true" };
            var data = await _dah.FetchDerivedModelAsync<dynamic>(query, parameters);
            if (data.Count > 0)
                return true;
            else
                return false;

        }

        public async Task<List<RegisterList>> Getrole(string email)
        {
            string query = @"select ar.""Name"" as role , au.""UserName"" as user_name from ""AspNetUsers"" as au
              join ""AspNetUserRoles"" as r on r.""UserId""=au.""Id""
             join ""AspNetRoles"" ar on ar.""Id""=r.""RoleId""
             where au.""Email""=@emails and au.""is_active""=@isactive";
            var parameters = new { emails = email, isactive = "true" };
            var data = await _dah.FetchDerivedModelAsync<RegisterList>(query, parameters);
            return data;
        }
    }
}
