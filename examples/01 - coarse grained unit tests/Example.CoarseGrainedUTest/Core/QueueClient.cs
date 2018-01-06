using System;
using System.Threading.Tasks;

namespace Example.CoarseGrainedUTest.Core
{
    public class QueueClient : IQueueClient
    {
        public Task PublishMessageAsync<T>(T message)
        {
            throw new NotImplementedException();
        }
    }
}
