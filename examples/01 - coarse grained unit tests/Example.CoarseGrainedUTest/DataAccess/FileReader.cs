using System.IO;
using System.Threading.Tasks;

namespace Example.CoarseGrainedUTest.DataAccess
{
    public class FileReader : IFileReader
    {
        public Task<string[]> GetFileDataAsync(string filePath) => Task.FromResult(File.ReadAllLines(filePath));
    }
}
