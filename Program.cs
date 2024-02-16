using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GettingStarted.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<GettingStartedMovieContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("GettingStartedMovieContext") ?? throw new InvalidOperationException("Connection string 'GettingStartedMovieContext' not found.")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();
