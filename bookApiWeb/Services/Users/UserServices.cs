using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using bookApiWeb.Configurations.Jwt;
using bookApiWeb.Models;
using bookApiWeb.Models.Users;
using bookApiWeb.Repositories.Users;
using bookApiWeb.Services.Users.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace bookApiWeb.Services.Users
{
    public class UserServices : IUserRepository
    {
        private readonly NoteContext _context = null;
        private readonly AppSettings _appSettings;

        public UserServices(IOptions<Settings> settings, IOptions<AppSettings> appSettings)
        {
            _context = new NoteContext(settings);
            _appSettings = appSettings.Value;
        }

        public Task<User> AddUser(User item)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Authencate(LoginRequest request)
        {
            var sss = request.Email;
            var user = await _context.Users.Find(item => item.Email == request.Email).FirstOrDefaultAsync();
            if (user == null) return null;

            var token = generateJwtToken(user);
            return token;

            //return false;
        }

        public string generateJwtToken(User user)
        {
            //generate token that us valid 7 day
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token) ;
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

        public User GetUser(string id)
        {
            try
            {
                return _context.Users.Find(user => user.UserId == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> Register(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
