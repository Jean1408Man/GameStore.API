using GameStore.Api.Data;
using GameStore.API.Models;
using Microsoft.EntityFrameworkCore;

static class DataExtensions
{
    public static void MigrateDB(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        context.Database.Migrate();
    }
    public static void AddGameStoreDB(this WebApplicationBuilder builder)
    {
        string? connString= builder.Configuration.GetConnectionString("GameStoreConnection");
        builder.Services.AddSqlite<GameStoreContext>(connString, optionsAction: options => options.UseSeeding((context, _) =>
        {
            if(!context.Set<Genre>().Any())
            {
                context.Set<Genre>().AddRange(
                    new Genre { Name = "Figthing" },
                    new Genre { Name = "RPG" },
                    new Genre { Name = "Platformer" },
                    new Genre { Name = "Racing" },
                    new Genre { Name = "Sports" }
                );
            }
            context.SaveChanges();
        }));

    }
}