using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookApiWeb.Repositories.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace bookApiWeb.Configurations.Jwt
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserRepository userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if(token != null)
            {
                attachUserToContent(context,userService,token);
            }

            await _next(context);

        }

        private void attachUserToContent(HttpContext context,IUserRepository userService, string token)
        {
            try
            {
                //var tokenHandler = new 
            }
            catch
            {
                //
            }
        }
    }
}
