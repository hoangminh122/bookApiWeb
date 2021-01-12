using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookApiWeb.Models.Users;
using bookApiWeb.Services.Users.dto;

namespace bookApiWeb.Repositories.Users
{
    public interface IUserRepository
    {
        Task<string> Authencate(LoginRequest request);
        Task<bool> Register(RegisterRequest request);

        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUser(string id);
    }
}
