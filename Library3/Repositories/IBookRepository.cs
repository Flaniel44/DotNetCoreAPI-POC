using Library3.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library3.Repositories
{
    public interface IBookRepository
    {
        //BOOKS
        Task<IEnumerable<BookItem>> GetAllBooks();
        Task<BookItem> GetBook(string id);

        // query after multiple parameters
        Task<IEnumerable<BookItem>> GetBook(string bodyText, DateTime updatedFrom, long headerSizeLimit);

        // add new note document
        Task AddBook(BookItem book);

        // remove a single document / note
        Task<bool> RemoveBook(string name);

        // update just a single document / note
        Task<bool> UpdateBook(string id, string body);

        //AUTHENTICATION
        Task<LoginModel> GetLogin(string userName, string password);

        //TRANSACTIONS
        Task<IEnumerable<TransactionItem>> GetAllTransactions();
        Task<IEnumerable<TransactionItem>> GetTransactionsByType(int type);
        Task<IEnumerable<TransactionItem>> GetTransactionsByUser(string userId);
        Task<bool> AddTransaction(TransactionItem transactionItem);
    }
}
