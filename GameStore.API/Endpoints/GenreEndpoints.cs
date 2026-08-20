using GameStore.Api.Data;
using GameStore.API.DTOs;
using GameStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Endpoints;

public static class GenreEndpoints
{
    public static void MapGenreEndpoints(this WebApplication app)
    {
        const string GetGenreEndpointName = "GetGenre";
        var group = app.MapGroup("/genres");

        group.MapGet("", async (GameStoreContext context) =>
        {
            var genre = await context.Genres.Select(Genre => new GenreDTO(
                Genre.Id,
                Genre.Name
            )).ToListAsync();
            return Results.Ok(genre);
        });

        group.MapGet("/{id}",async (int id, GameStoreContext context) =>
        {
            var genre = await context.Genres.FindAsync(id);
            if(genre==null) return Results.NotFound();

            var genreDetails = new GenreDTO(
                genre.Id,
                genre.Name
            );

            return Results.Ok(genreDetails);
        }).WithName(GetGenreEndpointName);

        group.MapPost("", async (CreateGenreDTO newGenre, GameStoreContext context) =>
        {
            Genre genre = new (){
                Name= newGenre.Name
            };

            context.Genres.Add(genre);
            await context.SaveChangesAsync();

            GenreDTO genreDetails = new(
                genre.Id,
                genre.Name
            );
            return Results.CreatedAtRoute(GetGenreEndpointName, new { id = genreDetails.Id }, genreDetails);
        });

        group.MapPut("/{id}", async (int id, CreateGenreDTO updatedGenre, GameStoreContext context) =>
        {
            Genre? genre = await context.Genres.FindAsync(id);
            if (genre is null)
            {
                return Results.NotFound();
            }
            genre.Name = updatedGenre.Name;
            await context.SaveChangesAsync();
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, GameStoreContext context) =>
        {
            Genre? genre = await context.Genres.FindAsync(id);
            if (genre is null)
            {
                return Results.NotFound();
            }
            context.Genres.Remove(genre);
            await context.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}