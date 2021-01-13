using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bookApiWeb.Models.Users;
using bookApiWeb.Repositories.Users;
using bookApiWeb.Services.Users;
using bookApiWeb.Services.Users.dto;
using Microsoft.AspNetCore.Mvc;

namespace bookApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController :ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("/test/test")]
        public async Task<IActionResult> GetTest()
        {
            byte[] passwordHash = Encoding.ASCII.GetBytes("123456");
            byte[] secretSalt = Encoding.ASCII.GetBytes("minh123");
            //'var isTruePass = VerifyPasswordHash(request.Password, passwordHash, secretSalt);


            var hmac = new System.Security.Cryptography.HMACSHA512(secretSalt);
            //truoc khi hash code to string
              byte[]  passwordHashbyte = hmac.ComputeHash(passwordHash);
              string passwordHashstring = Encoding.ASCII.GetString(passwordHashbyte);

            //truoc khi hash code to string so 2

            byte[] passwordHashbyte2 = hmac.ComputeHash(passwordHash);
            string passwordHashstring2 = Encoding.ASCII.GetString(passwordHashbyte);


            //compare
            bool result = passwordHashstring == passwordHashstring2;


            //string passwordHashstring = Encoding.ASCII.GetString(passwordHashbyte);

            //return Ok("ssss");
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //return Ok("ssss");
            return Ok(await _userRepository.GetAllUsers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            //return Ok("ssss");
            return Ok(await _userRepository.GetUserAsync(id));
        }
        //[HttpGet]
        //public async Task<IActionResult> GetByEmail([FromQuery]string email)
        //{
        //    //return Ok("ssss");
        //    return Ok(await _userRepository.GetUserByEmailAsync(email));
        //}


        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(LoginRequest loginRequest)
        {
            var ssss = loginRequest.Email;
            string response = await _userRepository.Authencate(loginRequest);
            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect." });
            }
            return Ok(new { token = response });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            if (string.IsNullOrWhiteSpace(registerRequest.Password))
                return BadRequest(new { message = "Password is required" });
            //find user
            var user = await _userRepository.GetUserByEmailAsync(registerRequest.Email);
            if (user != null)
            {
                return BadRequest(new { message = "Email is already taken" });
            }

            var isSaved = await _userRepository.Register(registerRequest);
            if(isSaved)
                return Ok(new { sucess = 1 });
            return BadRequest();


        }

    }
}
