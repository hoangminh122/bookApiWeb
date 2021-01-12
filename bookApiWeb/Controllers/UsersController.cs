using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookApiWeb.Models.Users;
using bookApiWeb.Repositories.Users;
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //return Ok("ssss");
            return Ok(await _userRepository.GetAllUsers());
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(LoginRequest loginRequest)
        {
            var ssss = loginRequest.Email;
            var response = await _userRepository.Authencate(loginRequest);
            if(response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect." });
            }
            return Ok(response);
        }



    }
}
