using System;
using System.Collections.Generic;
using System.Text;

namespace Example.InMemoryDependencies.Models
{
    public class Movie
    {
        public int Id { get; }
        public string Director { get; }
        public string Title { get; }

        public Movie(int id, string director, string title)
        {
            Id = id;
            Director = director;
            Title = title;
        }
    }
}
