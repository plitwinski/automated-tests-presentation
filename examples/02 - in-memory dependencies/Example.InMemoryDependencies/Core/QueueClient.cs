using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Example.InMemoryDependencies.Core
{
    public class QueueClient : IQueueClient
    {
        public Task PublishMessageAsync<T>(T message)
        {
            throw new NotImplementedException();
        }
    }
}
