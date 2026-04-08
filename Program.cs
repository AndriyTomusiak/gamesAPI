using Microsoft.EntityFrameworkCore;
using gamesApi.Data;
using gamesApi.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<GamesContext>(options =>
    options.UseSqlite("Data Source=games.db"));

var app = builder.Build();

app.UseCors();

app.UseStaticFiles();

app.MapGet("/", () => Results.Redirect("/admin/index.html"));

app.MapAdminRoutes();

app.Run("http://localhost:5100");
