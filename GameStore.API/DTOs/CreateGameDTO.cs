using System.ComponentModel.DataAnnotations;

namespace GameStore.API.DTOs;
public record CreateGameDTO(
    [Required][StringLength(100)]string Name,
    [Required][StringLength(100)]string Genre,
    [Required][Range(0, double.MaxValue)]decimal Price,
    [Required]DateOnly ReleaseDate
);

