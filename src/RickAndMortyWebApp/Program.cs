using Microsoft.EntityFrameworkCore;
using RickAndMortyWebApp;
using RickAndMortyWebApp.Data;
using RickAndMortyWebApp.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<RickAndMortyWebAppContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

builder.Services.AddScoped<ICharacterService, CharacterService>();
// NOTE: there are various ways to use cache, like redis cache if we would like to support scaling-up,
// or inmemory cache if we would like to cache the response etc.
// I choosed output cache for simplicity
builder.Services.AddOutputCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseMiddleware<DatabaseHeaderMiddleware>();

app.UseOutputCache();

app.MapGet("/", context =>
{
    context.Response.Redirect("/Characters");
    return Task.CompletedTask;
});

app.Run();
