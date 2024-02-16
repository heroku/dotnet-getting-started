using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GettingStarted.Data;
using System.Text.RegularExpressions;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<GettingStartedMovieContext>(options =>
{
    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
    {
        var m = Regex.Match(Environment.GetEnvironmentVariable("DATABASE_URL")!, @"postgres://(.*):(.*)@(.*):(.*)/(.*)");
        options.UseNpgsql($"Server={m.Groups[3]};Port={m.Groups[4]};User Id={m.Groups[1]};Password={m.Groups[2]};Database={m.Groups[5]};sslmode=Prefer;Trust Server Certificate=true");
    }
    else
    {
        // Use sqlite locally
        options.UseSqlite(builder.Configuration.GetConnectionString("GettingStartedMovieContext") ?? throw new InvalidOperationException("Connection string 'GettingStartedMovieContext' not found."));
    }
});

var app = builder.Build();

// AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

if (!app.Environment.IsDevelopment())
{
    //   app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();
