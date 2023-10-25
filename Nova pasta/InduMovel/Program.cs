using System.Configuration;
using InduMovel.Context;
using InduMovel.Models;
using InduMovel.Repositories;
using InduMovel.Repositories.Interfaces;
using InduMovel.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ConfiguraImagem>(builder.Configuration.GetSection("ConfImagemItem"));
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaulConnection")));
builder.Services.AddIdentity<UserAcount, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IMovelRepository, MovelRepository>();
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();


builder.Services.AddScoped(sp => Carrinho.GetCarrinhoCompra(sp));

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
app.UseSession();

app.UseRouting();
CriarPerfisUsuarios(app);


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

static void CriarPerfisUsuarios(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using var scope = scopedFactory?.CreateScope();
    var service = scope?.ServiceProvider.GetService<ISeedUserRoleInitial>();
    service?.SeedRoles();
    service?.SeedUsers();
}