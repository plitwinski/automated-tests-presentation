using System.Threading.Tasks;

namespace Example.CoarseGrainedUTest
{
    public interface IFileReader
    {
        Task<string[]> GetFileDataAsync(string filePath);
    }
}