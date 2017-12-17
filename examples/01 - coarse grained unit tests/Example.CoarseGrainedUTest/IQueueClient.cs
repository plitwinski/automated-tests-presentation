using System.Threading.Tasks;

namespace Example.CoarseGrainedUTest
{
    public interface IQueueClient
    {
        Task PublishMessageAsync<T>(T message);
    }
}