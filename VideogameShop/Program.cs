using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VideogameShop.Database;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VideogameContext>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<VideogameContext>();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();



var scope = app.Services
    .GetService<IServiceScopeFactory>()?
    .CreateScope();

if (scope is not null)
{
    using (scope)
    {
        var roleManger = scope
            .ServiceProvider
            .GetService<RoleManager<IdentityRole>>();

        if(roleManger.Roles.Count() == 0)
        {
            await roleManger.CreateAsync(new IdentityRole("Admin"));
            await roleManger.CreateAsync(new IdentityRole("Cliente"));
        }
    }
}

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
