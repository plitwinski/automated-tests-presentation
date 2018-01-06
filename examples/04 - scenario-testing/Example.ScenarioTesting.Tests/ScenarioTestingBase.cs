using NUnit.Framework;
using System.Threading.Tasks;

namespace Example.ScenarioTesting.Tests
{
    public abstract class ScenarioTestingBase
    {
        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            await Given();
            await When();
        }

        public virtual Task Given()
        {
            return Task.CompletedTask;
        }

        public abstract Task When();
    }
}
