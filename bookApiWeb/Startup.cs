using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using bookApiWeb.Configurations.Jwt;
using bookApiWeb.Models;
using bookApiWeb.Repositories;
using bookApiWeb.Repositories.Students;
using bookApiWeb.Repositories.Users;
using bookApiWeb.Services;
using bookApiWeb.Services.Students;
using bookApiWeb.Services.Users;
using bookApiWeb.Services.Users.dto;
using bookApiWeb.Shares.Exceptions;
using bookApiWeb.Shares.Exeptions;
using bookApiWeb.Shares.Swaggers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace bookApiWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddTransient<IUserRepository, UserServices>();
            services.AddTransient<INoteRepository, NotesServices>();
            services.AddTransient<IStudentRepository, StudentServices>();
          
            services.AddMvc()
                .AddFluentValidation();

            services.AddTransient<IValidator<LoginRequest>,LoginRequestValidator>();
            //.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            //.ConfigureApiBehaviorOptions(options =>
            //{
            //    options.InvalidModelStateResponseFactory = context =>
            //    {
            //        var problems = new CustomBadRequest(context);
            //        return new BadRequestObjectResult(problems);
            //    };
            //});

            services.Configure<Settings>(options =>
            {
                options.ConnectionString
                    = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database
                    = Configuration.GetSection("MongoConnection:Database").Value;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Learning .NET CORE",
                    Description = "AAAAAAAA"

                });

                c.OperationFilter<SwaggerFileOperationFilter>();

                //bearer
                c.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
                {

                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme{
                        Reference = new OpenApiReference{
                            Id = "Bearer", //The name of the previously defined security scheme.
                            Type = ReferenceType.SecurityScheme
                        }
                    },new List<string>()
                }
});

                //Set the comment path for the swagger JSON AND UI
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath,includeControllerXmlComments:true);
            });

            
            


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {


            //enable middleware to serve
            app.UseSwagger();
            app.UseSwaggerUI((c)=>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","My API");
                c.RoutePrefix = "";
            });

            //if (env.IsDevelopment())
            //{

            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler(
            //        options =>
            //        {
            //            options.Run(async context =>
            //            {
            //                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //                var ex = context.Features.Get<IExceptionHandlerFeature>();
            //                if(ex != null)
            //                {
            //                    await context.Response.WriteAsync(ex.Error.Message);
            //                }
            //            });
            //        }
            //        );
            //}

            //app.UseExceptionHandler(
            //    options =>
            //    {
            //        options.Run(async context =>
            //        {
            //            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //            var ex = context.Features.Get<IExceptionHandlerFeature>();
            //            if (ex != null)
            //            {
            //                await context.Response.WriteAsync(ex.Error.Message);
            //            }
            //        });
            //    });
            //

           
            app.ConfigureExceptionHandler(env);
           
            app.UseHttpsRedirection();
            app.UseMiddleware<JwtMiddleware>();

            app.UseMvc();
            app.UseStaticFiles();
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            );
            //app.UseMiddleware<JwtMiddleware>();
            //app.UseMigrationsEndPoint();


            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("asdahsgd");
            //});
        }
    }
}
