using System.ComponentModel.DataAnnotations;

namespace GameStore.API.DTOs;
public record CreateGameDTO(
    [Required][StringLength(100)]string Name,
    [Required]int GenreId,
    [Required][Range(0, double.MaxValue)]decimal Price,
    [Required]DateOnly ReleaseDate
);

