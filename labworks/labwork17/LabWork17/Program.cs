using AuthLibrary.Contexts;
using AuthLibrary.Services;
using CinemaClassLibrary.Contexts;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<AuthService>();

builder.Services.AddDbContext<CinemaDbContext>();
builder.Services.AddDbContext<CinemaUserDbContext>();

CultureInfo.DefaultThreadCurrentCulture =
    new("ru-RU") { NumberFormat = { NumberDecimalSeparator = "." } };

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
