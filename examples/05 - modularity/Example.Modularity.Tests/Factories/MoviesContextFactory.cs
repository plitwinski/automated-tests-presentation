using Example.InMemoryDependencies.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Example.Modularity.Tests.Factories
{
    internal class MoviesContextFactory
    {
        private readonly MoviesContext _context;

        public MoviesContextFactory(string databaseName = null)
        {
            _context = new MoviesContext(CreateInMemoryOptions(databaseName));
        }

        public MoviesContextFactory AddEntities<TEntity>(params TEntity[] entities) where TEntity : class
        {
            foreach (var entity in entities)
                _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return this;
        }

        public MoviesContext Create()
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
