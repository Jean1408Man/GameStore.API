using GameStore.API.DTOs;

namespace GameStore.API.Endpoints;

public static class GameEndpoints
{
    public static void MapGameEndpoints(this WebApplication app)
    {
        const string GetGameEndpointName = "GetGame";
        var group = app.MapGroup("/games");
        List<GameDTO> games = new()
        {
            new GameDTO(1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15))
        };

        group.MapGet("", () => games);

        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(game => game.Id == id);
            return game is not null ? Results.Ok(game) : Results.NotFound();
        }).WithName(GetGameEndpointName);

        group.MapPost("", (CreateGameDTO createGameDTO) =>
        {
            var game = new GameDTO(
                games.Count + 1,
                createGameDTO.Name,
                createGameDTO.Genre,
                createGameDTO.Price,
                createGameDTO.ReleaseDate
            );

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        group.MapPut("/{id}", (int id, UpdateGameDTO updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }
            games[index] = new GameDTO(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );
            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {
            var index = games.FindIndex(game => game.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }
            games.RemoveAt(index);
            return Results.NoContent();
        });
    }
}