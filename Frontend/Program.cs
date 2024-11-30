using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using GettingStarted.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
var isHeroku = Environment.GetEnvironmentVariable("DYNO") != null;
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    if (isHeroku)
    {
        options.KnownNetworks.Clear();
        options.KnownProxies.Clear();
    }
});
builder.Services.AddHttpsRedirection(options =>
{
    if (isHeroku)
    {
        options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
        options.HttpsPort = 443;
    };
});
builder.Services.AddDbContext<GettingStartedMovieContext>(options =>
{
    var match = Regex.Match(Environment.GetEnvironmentVariable("DATABASE_URL") ?? "", @"postgres://(.*):(.*)@(.*):(.*)/(.*)");
    options.UseNpgsql($"Server={match.Groups[3]};Port={match.Groups[4]};User Id={match.Groups[1]};Password={match.Groups[2]};Database={match.Groups[5]};sslmode=Prefer;Trust Server Certificate=true");
});

var app = builder.Build();

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
