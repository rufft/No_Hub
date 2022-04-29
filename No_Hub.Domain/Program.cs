using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using No_Hub.Domain.Data;
using No_Hub.Domain.Services.Classes;
using No_Hub.Domain.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;


// Add services to the container.


services.AddControllersWithViews();
services.AddServerSideBlazor();

services.AddScoped<ISetupManagerService, SetupManager>();
services.AddScoped<IProjectManager, ProjectManager>();

services.ConfigureApplicationCookie(opts => {
    opts.Cookie.Name = ".AspNet.SharedCookie";
});

services.AddDbContext<DataContext>(opts => {
    opts.UseNpgsql(configuration.GetConnectionString("no_hub"));
});

services.AddDefaultIdentity<IdentityUser>(opts => {
    opts.Password.RequiredLength = 5;
    opts.Password.RequireDigit = true;
    opts.Lockout.MaxFailedAccessAttempts = 5;
    opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
    opts.User.RequireUniqueEmail = true;
    opts.SignIn.RequireConfirmedEmail = false;
    opts.SignIn.RequireConfirmedAccount = false;
    opts.SignIn.RequireConfirmedPhoneNumber = false;
})
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<DataContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapBlazorHub();
app.MapFallbackToPage("/roles", "/Setup/SetupIndex");
app.MapFallbackToPage("/users/setup", "/Setup/SetupIndex");
    
app.Run();
