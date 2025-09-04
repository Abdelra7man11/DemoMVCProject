using Demo.BusinessLogic.Profiles;
using Demo.BusinessLogic.Services;
using Demo.BusinessLogic.Services.DepartmentServices;
using Demo.BusinessLogic.Services.EmployeeServices;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.DepartmentsModel;
using Demo.DataAccess.Repositories.Classes;
using Demo.DataAccess.Repositories.DepartmentsRepositery;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Demo.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Services
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

            });

            //builder.Services.AddScoped<ApplicationDbContext>();   // Register To Services In DI Container

            // Another Way To Register

            builder.Services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    //options.UseSqlServer("Connection String");// The One Way 
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // The Two Way From appsettings.json
                    options.UseLazyLoadingProxies();

                }
                );

            builder.Services.AddScoped<IDepartmentRepositery, DepartmentRepositery>(); // 2. Register to CLR Dependency Injection
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            // 2. Register to CLR Dependency Injection IEmployeesReposiory
            builder.Services.AddScoped<IEmployeesReposiory, EmployeesRepository>();
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();

            // Auto Mapper
            //builder.Services.AddAutoMapper(typeof(Department).Assembly);  // The Mapper in Any Class Related in Project 
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));  // The Mapper in Class profile


            #endregion

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
