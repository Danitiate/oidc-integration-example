using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIdConnectIntegrationExample.Database;
using OpenIdConnectIntegrationExample.Service;

var builder = WebApplication.CreateBuilder(args);

// Configure database
var sqliteConnectionString = builder.Configuration.GetConnectionString("Sqlite");
builder.Services.AddDbContext<SampleDbContext>(
    dbContextOptions => dbContextOptions
        .UseSqlite(sqliteConnectionString)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ConfigurationService>();
builder.Services.AddTransient<ConfigurationRepository>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<SampleDbContext>();
context.Database.Migrate();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(options =>
  options.WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
