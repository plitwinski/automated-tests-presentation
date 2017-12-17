using System.ComponentModel.DataAnnotations;

namespace Example.InMemoryDependencies.DataAccess
{
    public class MovieEntity
    {
        [Key]
        public int Id { get; set; }
        public string Director { get; set; }
        public string Title { get; set; }
    }
}
