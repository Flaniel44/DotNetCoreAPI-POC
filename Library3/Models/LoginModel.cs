using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library3.Models
{
    //LoginModel to hold user’s credentials on the server.
    public class LoginModel
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
