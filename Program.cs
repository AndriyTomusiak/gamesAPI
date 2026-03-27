using gamesApi.Data;
using gamesApi.Routes;

var builder = WebApplication.CreateBuilder(args);

// Реєструємо сховище як Singleton (одне на весь додаток)
builder.Services.AddSingleton<GamesStore>();

var app = builder.Build();

app.MapGet("/", () => "Games API is running");

// Підключаємо роути
app.MapPublicRoutes();   // GET  /api/games, /api/games/{id}
app.MapAdminRoutes();    // POST/PUT/DELETE /api/admin/games

app.Run();
