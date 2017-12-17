using System.IO;
using System.Threading.Tasks;

namespace Example.CoarseGrainedUTest
{
    public class FileReader : IFileReader
    {
        public Task<string[]> GetFileDataAsync(string filePath) => Task.FromResult(File.ReadAllLines(filePath));
    }
}
