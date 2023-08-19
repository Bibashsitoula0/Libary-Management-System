using BookHive.Dal.AccountRepository;
using BookHive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookhive.Service.AccountService
{
    public class AccountService : IAccountService
    {
        public readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {

            _accountRepository = accountRepository;
        }

        public async Task<bool> DeleteUser(string Id)
        {
            return await _accountRepository.DeleteUser(Id);
        }

        public async Task<bool> GetCheckActiveUser(string user_id)
        {
            var data = await _accountRepository.GetCheckActiveUser(user_id);
            return data;
        }

        public async Task<List<RegisterList>> Getrole(string email)
        {
            var data = await _accountRepository.Getrole(email);
            return data;
        }

        public async Task<List<RegisterList>> RegisterGeneralUserList()
        {
            var data = await _accountRepository.RegisterGeneralUserList();
            return data;
        }

        public async Task<List<RegisterList>> StudentUserList()
        {
            var data = await _accountRepository.StudentUserList();
            return data;
        }
    }
}
