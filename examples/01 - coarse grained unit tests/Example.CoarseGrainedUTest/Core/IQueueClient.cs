using System.Threading.Tasks;

namespace Example.CoarseGrainedUTest.Core
{
    public interface IQueueClient
    {
        Task PublishMessageAsync<T>(T message);
    }
}