using HostelWebAPI.DataAccess.Interfaces;
using HostelWebAPI.DataAccess.Repositories;
using HostelWebAPI.Models;
using HostelWebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelWebAPI
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

            services.AddDbContext<HostelDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultCnn"));
            });

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<HostelDBContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IDbRepo, DbRepo>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen();

            // ==== Add JWT Authentication ==== 
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // clear all default claim types
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AppPolicies.RequiredOwnerRole, policy => policy.RequireRole(new[] { AppRoles.Owner }));
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.User.RequireUniqueEmail = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider isp)
        {
            // Setup roles
            CreateRoleAsync(isp, AppRoles.User).Wait();
            CreateRoleAsync(isp, AppRoles.Owner).Wait();
            CreateRoleAsync(isp, AppRoles.Admin).Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(opt =>
            {
                //opt.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
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

        private async Task CreateRoleAsync(IServiceProvider isp, string roleName)
        {
            var roleManager = isp.GetRequiredService<RoleManager<IdentityRole>>();
            if(await roleManager.FindByNameAsync(roleName) == null){
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
