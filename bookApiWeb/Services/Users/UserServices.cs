using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookApiWeb.Models;
using bookApiWeb.Models.Users;
using bookApiWeb.Repositories.Users;
using bookApiWeb.Services.Users.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace bookApiWeb.Services.Users
{
    public class UserServices : IUserRepository
    {
        private readonly NoteContext _context = null;

        public UserServices(IOptions<Settings> settings)
        {
            _context = new NoteContext(settings);
        }

        public Task<User> AddUser(User item)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Authencate(LoginRequest request)
        {
            var result = await _context.Users.Find(item => item.Email == request.Email).FirstOrDefaultAsync();
            var n = 1;
            return result;

            //return false;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return await _context.Users.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<User> GetUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Register(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
