using TestWebApp.BLL.Repositories.Entities.Interfaces;
using TestWebApp.BLL.Repositories.Entities.Implement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TestWebApp.Common.Helpers.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TestWebApp.DAL.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TestWebApp.DAL.Data;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TestWebApp.BLL.Services.Identity.Interfaces;
using TestWebApp.BLL.Services.Identity.Implement;

namespace TestWebApp.WebAPI
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
            services.AddMemoryCache();
            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Us API V1",
                    Description = "Test ASP.NET Core application for joint project"
                });
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection"),
                    options => options.MigrationsAssembly("TestWebApp.DAL"));
            });

            var jwtAuthConfig = Configuration.Get<JwtAuth>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtAuthConfig.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtAuthConfig.Audience,

                        ValidateLifetime = true,

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuthConfig.AccessTokenSecret)),
                        ValidateIssuerSigningKey = true
                    };
                });

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IIdentityService, IdentityService>();
            services.AddSingleton<IRefreshTokenService, RefreshTokenService>();
            services.AddSingleton<TokenManager>();
            services.AddScoped<Authenticator>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Us API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}