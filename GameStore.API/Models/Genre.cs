using Microsoft.EntityFrameworkCore;
namespace GameStore.API.Models;

[Index(nameof(Name), IsUnique = true)]
class Genre
{
    public int Id { get; set; }
    public required string Name { get; set; }
}