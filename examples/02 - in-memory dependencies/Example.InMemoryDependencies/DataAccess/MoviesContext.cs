using Microsoft.EntityFrameworkCore;

namespace Example.InMemoryDependencies.DataAccess
{
    public class MoviesContext : DbContext
    {
        public MoviesContext(DbContextOptions options)
            :base(options)
        {

        }

        public virtual DbSet<MovieEntity> Movies { get; set; }
    }
}
