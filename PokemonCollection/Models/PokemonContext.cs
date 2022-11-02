using Microsoft.EntityFrameworkCore;

namespace PokemonCollection.Models
{
    public class PokemonContext : DbContext
    {
        public PokemonContext(DbContextOptions<PokemonContext> options)
            : base(options)
        {
        }

        public DbSet<PokemonItem> PokemonItems { get; set; }
    }
}
