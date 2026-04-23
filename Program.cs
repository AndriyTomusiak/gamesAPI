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

var app = builder.Build();

app.UseCors();

app.UseStaticFiles();

app.MapGet("/", () => Results.Redirect("/admin/index.html"));

app.MapMapRoutes();

app.Run();
