namespace gamesApi.Routes;

public static class AdminRoutes
{
    public static void MapAdminRoutes(this WebApplication app)
    {
        var admin = app.MapGroup("/api/admin");

        admin.MapAdminCarRoutes();
        admin.MapAdminDealerRoutes();
        admin.MapAdminUploadRoutes();
    }
}
