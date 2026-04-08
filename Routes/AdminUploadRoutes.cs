namespace gamesApi.Routes;

public static class AdminUploadRoutes
{
    public static void MapAdminUploadRoutes(this RouteGroupBuilder admin)
    {
        admin.MapPost("/upload", async (IFormFile file, IWebHostEnvironment env) =>
        {
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowed.Contains(ext))
                return Results.BadRequest(new { Message = "Allowed formats: jpg, jpeg, png, gif, webp" });

            if (file.Length > 5 * 1024 * 1024)
                return Results.BadRequest(new { Message = "Maximum file size: 5 MB" });

            var fileName = $"{Guid.NewGuid()}{ext}";
            var imagesPath = Path.Combine(env.WebRootPath, "images");
            Directory.CreateDirectory(imagesPath);

            var filePath = Path.Combine(imagesPath, fileName);
            await using var stream = File.Create(filePath);
            await file.CopyToAsync(stream);

            return Results.Ok(new { Url = $"/images/{fileName}" });
        }).DisableAntiforgery();
    }
}
