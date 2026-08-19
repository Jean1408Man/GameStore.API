using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;

static class DataExtensions
{
    public static void MigrateDB(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        context.Database.Migrate();
    }
}