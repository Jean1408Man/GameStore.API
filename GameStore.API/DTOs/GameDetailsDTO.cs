namespace GameStore.API.DTOs;

public record GameDetailsDTO(
    int Id,
    string Name,
    int GenreId,
    decimal Price,
    DateOnly ReleaseDate
);
