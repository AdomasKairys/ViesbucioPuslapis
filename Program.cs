using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ViesbucioPuslapis.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();



builder.Services.AddDbContext<HotelDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Default");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

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

app.Run();
