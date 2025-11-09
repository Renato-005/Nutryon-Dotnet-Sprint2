
using Nutryon.Application;
using Nutryon.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// DI reuse from API: Infra + App
var conn = builder.Configuration.GetConnectionString("NutryonDb");
builder.Services.AddInfrastructure(conn);
builder.Services.AddApplicationServices();

// CORS not necessary for same app; add routing
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
