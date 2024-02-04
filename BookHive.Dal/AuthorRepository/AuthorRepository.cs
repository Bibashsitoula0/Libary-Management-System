using BookHive.Dal.AccountRepository;
using BookHive.Dal.DapperConfigure;
using BookiHive.Model;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Slapper.AutoMapper;

namespace BookHive.Dal.AuthorRepository
{
    
    public class AuthorRepository : DALConfig, IAuthorRepository
    {
        public readonly IDataAccessLayer _dah;
        public AuthorRepository(IDataAccessLayer dah)
        {
            _dah = dah;

        }

        public async Task<int> AddAuthor(AuthorList model)
        {
            using (IDbConnection db = GetDbConnection())
            {
                var id = await db.InsertAsync(model);
                return id;
            }
        }

        public async Task<int> AddBook(Book model)
        {
            using (IDbConnection db = GetDbConnection())
            {
                var id = await db.InsertAsync(model);
                return id;
            }
        }

        public async Task<bool> ApproveRequest(int request, int cardId, int bookid) 
        { 
            string query = @"update cart set request=@Request ,isapproved=@approved where 
                        id=@cardId and bookid=@bookid                  
                ";
            var parameters = new
            {
                request = request,
                cardId = cardId,
                Bookid = bookid,
                approved = 0,
            };
            var data = await _dah.FetchDerivedModelAsync<dynamic>(query, parameters);
            return true;
        }

        public async Task<bool> ApproveStudent(bool approve, int cartid)
        {
            string query = @"update cart set isapproved=@approve where 
                          id=@cartid";
            var parameters = new
            {            
                approve = approve,
                cartid = cartid,
            };
            var data = await _dah.FetchDerivedModelAsync<dynamic>(query, parameters);
            return true;
        }

        public async Task<List<BookList>> BookList()
        {
            string query = @"exec get_book_list;";
            var user = await _dah.FetchDerivedModelAsync<BookList>(query);
            return user;
        }

        public async Task<bool> DeleteBook(int id)
        {
            string query = @"update books set isdeleted=@deleted where 
                        id = @id                       
                ";
            var parameters = new
            {
                id = id,
                deleted=true,
            };
            var data = await _dah.FetchDerivedModelAsync<dynamic>(query, parameters);
            return true;
        }

        public async Task<bool> DeleteCart(int Id, string userid)
        {
            string query = @"delete cart where 
                        id = @id and userid=@Userid                    
                ";
            var parameters = new
            {
                id = Id,
                Userid= userid
            };
            var data = await _dah.FetchDerivedModelAsync<dynamic>(query, parameters);
            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            string query = @"delete Author where 
                        id = @id                       
                ";
            var parameters = new
            {             
                id = id
            };
            var data = await _dah.FetchDerivedModelAsync<dynamic>(query, parameters);
            return true;
        }

        public async Task<List<AuthorList>> GetAuthor()
        {
            string query = @"select * from author";
            var user = await _dah.FetchDerivedModelAsync<AuthorList>(query);
            return user;
        }

        public async Task<List<Book>> GetBook(int id)
        {
            string query = @"select * from books where id=@id";
            var parameters = new { id = id };
            var user = await _dah.FetchDerivedModelAsync<Book>(query, parameters);
            return user;
        }

        public async Task<List<CartList>> getCart(string userid)
        {
            string query = @"exec get_cart_list @userid";
            var parameters = new { userid = userid };
            var user = await _dah.FetchDerivedModelAsync<CartList>(query, parameters);
            return user;
        }

        public async Task<List<Cart>> getCartList(int bookid, string userid)
        {
            string query = @"select * from cart where bookid=@Bookid and userid=@UserId";
            var parameters = new { Bookid = bookid,UserId= userid };
            var user = await _dah.FetchDerivedModelAsync<Cart>(query, parameters);
            return user;
        }

        public async Task<List<Faculty>> GetFaculty()
        {
            string query = @"select * from faculty";
            var user = await _dah.FetchDerivedModelAsync<Faculty>(query);
            return user;
        }

        public async Task<List<AuthorList>> GetList()
        {
            string query = @"select * from Author ORDER BY id DESC;";
            var user = await _dah.FetchDerivedModelAsync<AuthorList>(query);
            return user;
        }

        public async Task<List<Semister>> GetSemister()
        {
            string query = @"select * from semister";
            var user = await _dah.FetchDerivedModelAsync<Semister>(query);
            return user;
        }

        public async Task<List<Years>> GetYear()
        {
            string query = @"select * from years";
            var user = await _dah.FetchDerivedModelAsync<Years>(query);
            return user;
        }

        public async Task<bool> IsBookTaken(bool approve, int cartid)
        {
            
            string query = @"update cart set istaken=@approve where 
                        id=@cartid";
            var parameters = new
            {
                cartid= cartid,
                approve = approve
            };
            var data = await _dah.FetchDerivedModelAsync<dynamic>(query, parameters);
            return true;
        }

        public async Task<int> SaveCart(Cart model)
        {
            using (IDbConnection db = GetDbConnection())
            {
                var id = await db.InsertAsync(model);
                return id;
            }
        }

        public async Task<bool> UpdateBook(Book model)
        {
            using (IDbConnection db = GetDbConnection())
            {
               await db.UpdateAsync(model);
                return true;
            }
        }
    }
}
