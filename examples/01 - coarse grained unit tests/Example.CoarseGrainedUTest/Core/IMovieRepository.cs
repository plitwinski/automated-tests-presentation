﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Example.CoarseGrainedUTest.Models;

namespace Example.CoarseGrainedUTest.Core
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetMoviesAsync(string cinema);
    }
}