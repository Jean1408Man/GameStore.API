using System.ComponentModel.DataAnnotations;

namespace GameStore.API.DTOs;


public record GenreDTO(
    int Id,
    string Name
);

public record CreateGenreDTO(
    [Required][StringLength(50)]string Name
);
