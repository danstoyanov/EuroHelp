using EuroHelp.Data;
using EuroHelp.Services.Damages;
using EuroHelp.Services.InsuranceCompanies;
using EuroHelp.Services.References;
using EuroHelp.Services.Users;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .AddDbContext<EuroHelpDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services
    .AddDatabaseDeveloperPageExceptionFilter(); 

builder.Services
    .AddDefaultIdentity<IdentityUser>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    })
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
    .UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();


/*  Tasks:
 *  
 *  
 *  [-] Add Model statements in every controller action ! 
 *      [-] Damages
 *      [-] Reference
 *      
 *      [x] Consumer
 *      [x] Companies
 *      [x] Employee
 *      
 *  [-] Add checks for every action when something is not valid !
 *  [-] Clear some of the controllers with only one or two actions !
 *  [-] Check the names of the:
 *      [-] Service methods     
 *      [-] Data models !   
 *      [-] View models !
 *      [-] Controller names ! 
 *      [-] Controller actions !     
 *  [-] Add the roles in the app !
 *  [-] Fix the pages when we have many listed objects !
 *  [-] Add listing Company view model with pictures and others !!!! 
 *  [-] 
 *  [-] 
 *  
 *  
 *  
 */