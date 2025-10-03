using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlayVault.Data;
using PlayVault.Models;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PlayVaultContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PlayVaultContext") ?? throw new InvalidOperationException("Connection string 'PlayVaultContext' not found.")));

builder.Services.AddAntiforgery();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<FormOptions>(o => {
    o.MultipartBodyLengthLimit = 3145728; // 3 MB
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedDataGames.Initialize(services);
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

app.UseAntiforgery();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
