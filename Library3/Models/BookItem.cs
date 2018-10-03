using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library3.Models
{
    public class BookItem
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public string Name { get; set; }
        public int ISBN { get; set; }
        public int TotalQty { get; set; }
        public int AvailableQty { get; set; }
        public string Genre { get; set; }
        //public IList<ObjectId> Borrowees { get; set; }


    }
}
