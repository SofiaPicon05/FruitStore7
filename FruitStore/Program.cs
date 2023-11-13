using FruitStore.Models.Entities;
using FruitStore.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FruteriaShopContext>(x => x.UseMySql("server=localhost;user=root;password=root;database=FruteriaShop",
	Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql")));

builder.Services.AddTransient<Repository<Categorias>>();
builder.Services.AddTransient<ProductosRepository>();


builder.Services.AddMvc();
var app = builder.Build();

app.MapControllerRoute(
    name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.UseFileServer();
app.MapDefaultControllerRoute();

app.Run();
