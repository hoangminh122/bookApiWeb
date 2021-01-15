using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace bookApiWeb.Shares.Exeptions
{
    class ErrorHandler
    {
        public int status { get; set; }
    }
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(
                    options =>
                    {
                        options.Run(async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            var ex = context.Features.Get<IExceptionHandlerFeature>();
                            if (ex != null)
                            {
                                var minh = new ErrorHandler { status = (int)HttpStatusCode.NotFound };
                                var error = JsonSerializer.Serialize<ErrorHandler>(minh);
                                //await context.Response.WriteAsync( ex.Error.Message);
                                await context.Response.WriteAsync(error);
                            }
                        });
                    }
                    );
            }
        }
    }
}
