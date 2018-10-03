using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library3.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Library3.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContextMongo _context = null;

        public BookRepository(IOptions<DBSettings> settings)
        {
            _context = new BookContextMongo(settings);
        }

        //CRUD operations

        public async Task AddBook(BookItem book)
        {
            try
            {
                await _context.Books.InsertOneAsync(book);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> AddTransaction(TransactionItem transactionItem)
        {
            try
            {
                string[] transactions = { "Returned", "Borrowed" };

                var transactionType = transactions.Contains(transactionItem.TransactionType);
                var bookName = await _context.Books.Find(book => book.Name.ToLower() == transactionItem.BookName.ToLower())
                                .FirstOrDefaultAsync();
                var userName = await _context.Users.Find(user => user.UserName.ToLower() == transactionItem.UserName.ToLower())
                                .FirstOrDefaultAsync();

                if (bookName != null && userName != null && transactionType)
                {
                    await _context.Transactions.InsertOneAsync(transactionItem);
                    return true;
                } else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<BookItem>> GetAllBooks()
        {
            try
            {
                return await _context.Books
                        .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<TransactionItem>> GetAllTransactions()
        {
            try
            {
                return await _context.Transactions
                        .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<BookItem> GetBook(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.Books
                                .Find(book => book.Name == id
                                        || book.InternalId == internalId)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public Task<IEnumerable<BookItem>> GetBook(string bodyText, DateTime updatedFrom, long headerSizeLimit)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginModel> GetLogin(string userName, string password)
        {
            try
            {
                var found = await _context.Users
                                .Find(user => user.UserName.Equals(userName)
                                        && user.Password.Equals(password))
                                .FirstOrDefaultAsync();
                return found;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<TransactionItem>> GetTransactionsByType(int type)
        {
            try
            {
                switch(type)
                {
                    case 1:
                        return await _context.Transactions
                                .Find(transaction => transaction.TransactionType.Equals("Borrowed"))
                                .ToListAsync();    
                    case 2:
                        return await _context.Transactions
                                .Find(transaction => transaction.TransactionType.Equals("Returned"))
                                .ToListAsync();
                    default:
                        throw new NotImplementedException();
                }
                
                 
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<TransactionItem>> GetTransactionsByUser(string userId)
        {
            try
            {
                return await _context.Transactions
                                .Find(transaction => transaction.UserName.ToLower() == userId.ToLower())
                                .ToListAsync();

            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveBook(string name)
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Books.DeleteOneAsync(
                        Builders<BookItem>.Filter.Eq("Name", name));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public Task<bool> UpdateBook(string id, string body)
        {
            throw new NotImplementedException();
        }

        //class methods
        private ObjectId GetInternalId(string id)
        {
            //Tries to parse a string and create a new ObjectId, otherwise returns empty
            if (!ObjectId.TryParse(id, out ObjectId internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

    }
}
