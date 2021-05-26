using HostelWebAPI.BL;
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
using Microsoft.OpenApi.Models;
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
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt => opt.AddPolicy(
                MyAllowSpecificOrigins,
                builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())
            );

            services.AddDbContext<HostelDBContext>(options =>
            {
                if (_env.IsProduction())
                {
                    options.UseSqlServer(Configuration.GetConnectionString("ProductionDb"));
                }
                else if (_env.IsDevelopment())
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DevelopmentDb"));
                }
            });

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<HostelDBContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IDbRepo, DbRepo>();
            services.AddScoped<IPropertyRepo, PropertyRepo>();
            services.AddScoped<ICityRepo, CityRepo>();
            services.AddScoped<IReservationHistoryRepo, ReservationHistoryRepo>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPropertyTypeRepo, PropertyTypeRepo>();
            services.AddScoped<IUserPropertyLikeRepo, UserPropertyLikeRepo>();
            services.AddScoped<IReviewRepo, ReviewRepo>();
            services.AddScoped<IDiscountBL, DiscountBL>();
            services.AddScoped<IUserService, UserService>();

            // services.AddSingleton<IReservationWorker, ReservationWorker>();

            services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization Bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                     {
                         new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference()
                             {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                             },
                             Scheme = "oauth2",
                             Name = "Bearer",
                             In = ParameterLocation.Header,
                         },
                         new List<string>()
                     }
                });
            });

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

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseExceptionHandler("/error");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private async Task CreateRoleAsync(IServiceProvider isp, string roleName)
        {
            var roleManager = isp.GetRequiredService<RoleManager<IdentityRole>>();
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
