using System.Threading.Tasks;

namespace Example.CoarseGrainedUTest.DataAccess
{
    public interface IFileReader
    {
        Task<string[]> GetFileDataAsync(string filePath);
    }
}