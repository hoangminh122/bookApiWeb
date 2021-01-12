using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Models.Users
{
    public class User
    {
        public User()
        {

        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsConnected { get; set; } = false;

        public bool IsDeleted { get; set; } = false;
        public DateTime? BirthDate { get; set; } = null;

        public DateTime LastModified { get; set; } = DateTime.Now;
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public BsonArray Pictures { get; set; } = new BsonArray(new List<string>());

    }
}
