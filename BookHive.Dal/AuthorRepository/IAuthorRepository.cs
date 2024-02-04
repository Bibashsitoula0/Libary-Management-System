using BookHive.Models;
using BookiHive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHive.Dal.AuthorRepository
{
    public interface IAuthorRepository
    {
        Task<List<Book>> GetBook(int id);
        Task<List<AuthorList>> GetList(); 
        Task<int> AddAuthor(AuthorList model);
        Task<bool> DeleteUser(int Id);
        Task<bool> DeleteBook(int Id);
        Task<List<BookList>> BookList();
        Task<List<Years>> GetYear();
        Task<List<Semister>> GetSemister();
        Task<List<Faculty>> GetFaculty();
        Task<List<AuthorList>> GetAuthor();
        Task<int> AddBook(Book model);
        Task<bool> UpdateBook(Book model);
        Task<int> SaveCart(Cart model); 
       Task<List<CartList>> getCart(string userid);
        Task<List<Cart>> getCartList(int bookid,string userid);
        Task<bool> DeleteCart(int Id,string userid); 
       Task<bool> ApproveRequest(int request,int cardId, int bookid);
        Task<bool> ApproveStudent( bool approve, int cartid);
        Task<bool> IsBookTaken(bool approve, int cartid);


    }
}
