using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Models.Users
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        [BsonElement("first_name")]
        public string FirstName { get; set; }
        [BsonElement("last_name")]
        public string LastName { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("connected")]
        public bool IsConnected { get; set; } = false;

        //public bool IsDeleted { get; set; } = false;
        //public DateTime? BirthDate { get; set; } = null;

        //public DateTime LastModified { get; set; } = DateTime.Now;
        //public DateTime RegisterDate { get; set; } = DateTime.Now;
        //public BsonArray Pictures { get; set; } = new BsonArray(new List<string>());

    }
}
