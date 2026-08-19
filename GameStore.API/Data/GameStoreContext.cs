using GameStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();
}