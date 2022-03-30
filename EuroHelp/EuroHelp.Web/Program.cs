using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using EuroHelp.Data;
using EuroHelp.Data.Models;
using EuroHelp.Services.Damages;
using EuroHelp.Services.InsuranceCompanies;
using EuroHelp.Services.References;
using EuroHelp.Services.Users;
using EuroHelp.Services.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .AddDbContext<EuroHelpDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services
    .AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddDefaultIdentity<User>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EuroHelpDbContext>();

builder.Services
    .AddControllersWithViews();

builder.Services
    .AddTransient<IDamageService, DamageService>();

builder.Services
    .AddTransient<ICompanyService, CompanyService>();

builder.Services
    .AddTransient<IReferenceService, ReferenceService>();

builder.Services
    .AddTransient<IUserService, UserService>();

var app = builder.Build();

app.PrepareDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/NotFound";
        await next();
    }
});

app.UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "Areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        endpoints.MapDefaultControllerRoute();
        endpoints.MapRazorPages();
    });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.UseAuthentication(); app.Run();


/*  Tasks:
 *  
 *  [-] Add checks for every action when something is not valid !
 *  [-] Clear some of the controllers with only one or two actions !
 *  [-] Check the names of the:
 *      [-] Service methods     
 *      [-] Data models !         
 *      [-] View models !          
 *      [-] Controller names ! 
 *      [-] Controller actions !     
 *  [-] Add ADMIN area with controllers and wiews !!!!
 *      [-] Add Admin Controller !
 *      [-] Add Admin View Pannel!
 *          [-] The admin user will have page for all information about:
 *              [-] All registered companies
 *              [-] All registered employees
 *              [-] All references 
 *              [-] All consumers
 *          [-] The admin can delete damages !
 *          [-] The admin can delete employee !
 *          [-] The admin can delete consumer !
 *          [-] The admin can delete insurance company !
 *          [-] When someone try to go in Admin page => return RedirectToAction("AccesDenied" "Home");
 *          [-] When the customer add new damage, the administrator must approve the registration.
 *          [-] When employee try to delete damage the admisnistator must approve this action. 
 *      [-] Add View Model !
 *      [-] Add Admin Service !
 *  [-] Database:
 *      [-] Add Reference to DB with {Id, Reference date, fromDate, toDate, Employee Id}
 *      [-] Add Types of Insurance companies {Id, Type, CompanyId}
 *      [-] Fix 
 *  [-] TempData: 
 *      [-] Add new damage !
 *      [-] Delete damage !
 *      [-] Add company !
 *      [-] Add employee !
 *      [-] Generate damage reference file by 'random' criteria !
 *  [-] Other Views:
 *      [-]
 *      [-]
 *  
 *  [-] Fix the pages when we have many listed objects !
 *  [-] Add listing Company view model with pictures and others !!!! 
 *  [-] When you add new compny check if the currCompany exists in Database !!!
 *  
 *  [x] Add the roles in the app !
 *  [x] Add Model statements in every controller action ! 
 *  [x] Reference
 *  [x] Damages
 *  [x] Consumer
 *  [x] Companies
 *  [x] Employee
 */