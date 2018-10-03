using Library3.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library3.Models
{
    public class BookContextMongo
    {
        private readonly IMongoDatabase _database = null;

        public BookContextMongo(IOptions<DBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        //Book collection
        public IMongoCollection<BookItem> Books
        {
            get
            {
                return _database.GetCollection<BookItem>("books");
            }
        }

        //User collection
        public IMongoCollection<LoginModel> Users
        {
            get
            {
                return _database.GetCollection<LoginModel>("users");
            }
        }

        //Transaction collection
        public IMongoCollection<TransactionItem> Transactions
        {
            get
            {
                return _database.GetCollection<TransactionItem>("transactions");
            }
        }
    }
}
