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

        public async Task<User> AddUser(User item)
        {
            try
            {
                #region test
                //var = "dd";
                #endregion
                await _context.Users.InsertOneAsync(item);
                return item;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> Authencate(LoginRequest request)
        {
            var sss = request.Email;
            var user = await _context.Users.Find(item => item.Email == request.Email).FirstOrDefaultAsync();
            if (user == null) return null;

            //compare two password
            var ssspass = user.Password;
            //byte[] passwordHash = Encoding.ASCII.GetBytes(user.Password);
            byte[] secretSalt = Encoding.ASCII.GetBytes(_appSettings.SecretCryptPassword);
            var isTruePass = VerifyPasswordHash(request.Password, user.Password,secretSalt);
            if(isTruePass)
            {
                var token = generateJwtToken(user);
                return token;
            }
            return null;
               

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
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
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

        public async Task<User> GetUserAsync(string id)
        {
            try
            {
                return await _context.Users.Find(user => user.UserId == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _context.Users.Find(user => user.Email == email).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            try
            {
                byte[] passwordHash = null;
                byte[] secretSalt = Encoding.ASCII.GetBytes(_appSettings.SecretCryptPassword);
                CreatePasswordHash(request.Password, out passwordHash, secretSalt);

                User userNew = new User()
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    IsConnected = true,
                    Password = Encoding.ASCII.GetString(passwordHash)
                };
                await AddUser(userNew);
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.ToString());
            }

        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, byte[] secretSalt)
        {
            if (password == null) throw new ArgumentNullException("password not null");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(secretSalt))
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.ASCII.GetBytes(password));
            }

        }

        private static bool VerifyPasswordHash(string password, string storedHash, byte[] secretSalt)
        {
            //storeHash get from database
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
          //  if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            //if (secretSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(secretSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.ASCII.GetBytes(password));

                //for (int i = 0; i < computedHash.Length; i++)
                //{
                //    var computedHashd = computedHash[i];
                //    var storedHashsss = storedHash[i];
                //    if (computedHash[i] != storedHash[i])
                //        return false;
                //}

                string computedHashString = Encoding.ASCII.GetString(computedHash);
                if (computedHashString != storedHash) return false;

            }
            return true;
        }
    }
}
