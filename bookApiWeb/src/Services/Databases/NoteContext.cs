using bookApiWeb.Models.Students;
using bookApiWeb.Models.Users;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Models
{
    public class NoteContext
    {
        private readonly IMongoDatabase _database = null;
        public NoteContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if(client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
            }
        }

        public IMongoCollection<Note> Notes
        {
            get
            {
                return _database.GetCollection<Note>("Note");
            }
        }
        public IMongoCollection<Student> Students
        {
            get
            {
                return _database.GetCollection<Student>("Student");
            }
        }
        public IMongoCollection<Course> Courses
        {
            get
            {
                return _database.GetCollection<Course>("Course");
            }
        }
        public IMongoCollection<NoteImage> NoteImages
        {
            get
            {
                return _database.GetCollection<NoteImage>("NoteImage");
            }
        }
        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("User");
            }
        }

    }
}
