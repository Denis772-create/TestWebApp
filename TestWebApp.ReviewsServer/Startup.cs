using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using TestWebApp.DAL.Data;
using TestWebApp.ReviewServer.Hubs;

namespace TestWebApp.ReviewServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddSignalR();
            services.AddControllers();

            services.AddCors(options =>
                options.AddPolicy("ShopUrlPolicy", builder =>
                {
                    builder.AllowCredentials();
                    builder.WithOrigins("https://localhost:44366/");
                }));

            services.AddDbContext<ApplicationDbContext>(options=>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly("TestWebApp.DAL")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "Us API V2",
                    Description = "Test ASP.NET Core application for joint project"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors("ShopUrlPolicy");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "Us API V2");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ReviewHub>("/review");
            });
        }
    }
}
