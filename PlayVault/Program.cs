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

// Configurazione caricamento file
builder.Services.Configure<FormOptions>(o => {
    o.MultipartBodyLengthLimit = 3145728; // 3 MB
});

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// ✅ Aggiunta: Cache distribuita in memoria per le sessioni
builder.Services.AddDistributedMemoryCache();

// ✅ Aggiunta: Configurazione della sessione
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".PlayVault.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

// ✅ Aggiunta: Abilita la sessione PRIMA dell'autorizzazione
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
