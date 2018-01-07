using Example.InMemoryDependencies.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Example.Modularity.Tests.Builders
{
    internal class MoviesContextBuilder
    {
        private readonly MoviesContext _context;

        public MoviesContextBuilder(string databaseName = null)
        {
            _context = new MoviesContext(CreateInMemoryOptions(databaseName));
        }

        public MoviesContextBuilder AddEntities<TEntity>(params TEntity[] entities) where TEntity : class
        {
            foreach (var entity in entities)
                _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return this;
        }

        public MoviesContext Build()
        {
            return _context;
        }

        private DbContextOptions CreateInMemoryOptions(string databaseName = null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MoviesContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName ?? "databaseName");
            return optionsBuilder.Options;
        }
    }
}
