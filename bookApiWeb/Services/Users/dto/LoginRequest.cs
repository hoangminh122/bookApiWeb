using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Services.Users.dto
{
    public class LoginRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}
