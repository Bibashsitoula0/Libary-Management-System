using BookHive.Models;
using BookiHive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookhive.Service.AuthorService
{
    public interface IAuthorService
    {
        Task<List<AuthorList>> GetList();
        Task<AuthorList> AddAuthor(AuthorList model);
        Task<bool> DeleteUser(int Id);

        Task<List<BookList>> BookList();


    }
}
