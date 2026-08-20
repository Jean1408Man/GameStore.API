using GameStore.Api.Data;
using GameStore.API.DTOs;
using GameStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Endpoints;

public static class GameEndpoints
{
    public static void MapGameEndpoints(this WebApplication app)
    {
        const string GetGameEndpointName = "GetGame";
        var group = app.MapGroup("/games");

        group.MapGet("", async (GameStoreContext context) =>
        {
            var games = await context.Games.Select(game => new GameDetailsDTO(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            )).ToListAsync();
            return Results.Ok(games);
        });

        group.MapGet("/{id}",async (int id, GameStoreContext context) =>
        {
            var game = await context.Games.FindAsync(id);
            if(game==null) return Results.NotFound();

            var gameDetails = new GameDetailsDTO(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );

            return Results.Ok(game);
        }).WithName(GetGameEndpointName);

        group.MapPost("", async (CreateGameDTO newGame, GameStoreContext context) =>
        {
            Game game = new (){
                Name= newGame.Name,
                GenreId= newGame.GenreId,
                Price= newGame.Price,
                ReleaseDate= newGame.ReleaseDate
            };

            context.Games.Add(game);
            await context.SaveChangesAsync();

            GameDetailsDTO gameDetails = new(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = gameDetails.Id }, gameDetails);
        });

        group.MapPut("/{id}", async (int id, UpdateGameDTO updatedGame, GameStoreContext context) =>
        {
            Game? game = await context.Games.FindAsync(id);
            if (game is null)
            {
                return Results.NotFound();
            }
            game.Name = updatedGame.Name;
            game.GenreId = updatedGame.GenreId;
            game.Price = updatedGame.Price;
            game.ReleaseDate = updatedGame.ReleaseDate;
            await context.SaveChangesAsync();
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, GameStoreContext context) =>
        {
            Game? game = await context.Games.FindAsync(id);
            if (game is null)
            {
                return Results.NotFound();
            }
            context.Games.Remove(game);
            await context.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}