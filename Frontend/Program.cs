using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using GettingStarted.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});
builder.Services.AddDbContext<GettingStartedMovieContext>(options =>
{
    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
    {
        var match = Regex.Match(Environment.GetEnvironmentVariable("DATABASE_URL")!, @"postgres://(.*):(.*)@(.*):(.*)/(.*)");
        options.UseNpgsql($"Server={match.Groups[3]};Port={match.Groups[4]};User Id={match.Groups[1]};Password={match.Groups[2]};Database={match.Groups[5]};sslmode=Prefer;Trust Server Certificate=true");
    }
    else
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("GettingStartedMovieContext") ?? throw new InvalidOperationException("Connection string 'GettingStartedMovieContext' not found."));
    }
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GettingStartedMovieContext>();
    db.Database.Migrate();
}

app.UseForwardedHeaders();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();
