using System.Threading.Tasks;

namespace Example.InMemoryDependencies.Core
{
    public interface IQueueClient
    {
        Task PublishMessageAsync<T>(T message);
    }
}