using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services;

namespace Web.Api.RoomRentService
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
            services.AddControllers();


            services.AddCors(options =>
            {
                options.AddPolicy("CORS",
                builder => builder
                .WithOrigins(
                 "http://localhost:4200",
                 "http://localhost:4201"
                )
                //.AllowAnyOrigin()
                // Apply CORS policy for any type of origin
                .AllowAnyMethod()
                // Apply CORS policy for any type of http methods
                .AllowAnyHeader()
                // Apply CORS policy for any headers
                .AllowCredentials());
                // Apply CORS policy for all users
            });

            //Add jwt authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Room_App_7676t_a978_jwttoken")),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //services.AddTransient<IRentService, RentService>();
            services.AddSingleton<IRentService>(new RentService("Room_App_7676t_a978_jwttoken"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("1.0.0", new OpenApiInfo
                {
                    Version = "1.0.0",
                    Title = "Reltal App",
                    Description = "Reltal App",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CORS");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/1.0.0/swagger.json", "Reltal App Service API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
