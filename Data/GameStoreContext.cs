using Microsoft.EntityFrameworkCore;
using GameStore.Api.Entities;

namespace GameStore.Api.Data
{
    public class GameStoreContext : DbContext
    {
        public GameStoreContext(DbContextOptions<GameStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games => Set<Game>();
    }
}
