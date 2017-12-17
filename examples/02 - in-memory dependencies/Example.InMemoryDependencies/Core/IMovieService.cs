using System.Collections.Generic;
using System.Threading.Tasks;
using Example.InMemoryDependencies.Models;

namespace Example.InMemoryDependencies.Core
{
    public interface IMovieService
    {
        Task<IEnumerable<CinemaUpdate>> CheckForChangesAsync();
    }
}