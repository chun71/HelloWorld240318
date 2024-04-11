
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using HelloWorld240318.Models;
using HelloWorld240318.Service;
using Microsoft.EntityFrameworkCore;

namespace HelloWorld240318
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /* DB Frist
             Scaffold-DbContext "Data Source=localhost;Database=MemoManager;User ID=Memo;Password=Memo@20240318;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -NoOnConfiguring -UseDatabaseNames -NoPluralize -Force
             */

            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // SqlServer ³s½u
            builder.Services.AddDbContext<MemoManagerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
            // Service ª`¤J
            builder.Services.AddScoped<IUsersDetailOneService, UsersDetailOneService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
