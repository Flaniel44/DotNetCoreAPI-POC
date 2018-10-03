using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library3.Models
{
    public class TransactionItem
    {
        [BsonId] //FYI, Having multiple of these results in internal error
        public ObjectId InternalId { get; set; }
        public string TransactionType { get; set; }
        public string UserName{ get; set; }
        public string BookName { get; set; }

    }
}
