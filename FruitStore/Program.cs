using FruitStore.Models.Entities;
using FruitStore.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FruteriashopContext>(x => x.UseMySql("server=localhost;user=root;password=root;database=FruteriaShop",
	Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql")));

builder.Services.AddTransient<Repository<Categorias>>();
builder.Services.AddTransient<ProductosRepository>();
builder.Services.AddTransient<Repository<Usuarios>>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.AccessDeniedPath = "/Home/Denied";
    x.LoginPath = "/Home/Login";
    x.LogoutPath = "/Home/Login";
    x.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    x.Cookie.Name = "fruteriaSofia";

});

builder.Services.AddMvc();
var app = builder.Build();

app.MapControllerRoute(
    name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.UseFileServer();
app.UseAuthentication();
app.UseAuthorization();


app.MapDefaultControllerRoute();

app.Run();
