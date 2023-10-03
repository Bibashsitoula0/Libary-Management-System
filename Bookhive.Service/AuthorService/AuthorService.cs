using BookHive.Dal.AuthorRepository;
using BookiHive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookhive.Service.AuthorService
{ 
   
    public class AuthorService : IAuthorService
    {
        public readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<AuthorList> AddAuthor(AuthorList model)
        {
            var id = await _authorRepository.AddAuthor(model);
            return model;
        }

        public async Task<List<AuthorList>> GetList()
        {
            return  await _authorRepository.GetList();
           
        }
        public async Task<bool> DeleteUser(int Id)
        {
            return await _authorRepository.DeleteUser(Id);
        }

        public async Task<List<BookList>> BookList()
        {
            return await _authorRepository.BookList();
        }
    }
}
