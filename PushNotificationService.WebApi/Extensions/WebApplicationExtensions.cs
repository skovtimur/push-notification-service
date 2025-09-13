using Microsoft.EntityFrameworkCore;
using PushNotificationService.Infrastructure;

namespace PushNotificationService.WebApi.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<MainDbContext>();
        context.Database.Migrate();

        return app;
    }
}