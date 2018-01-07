using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;

namespace Example.Modularity.Tests.Factories
{
    public class ServerFactory
    {
        private readonly IDictionary<Type, object> mockedServices;

        public ServerFactory()
        {
            mockedServices = new Dictionary<Type, object>();
        }

        public ServerFactory AddMock<TMock>(TMock mockedObject) where TMock : class
        {
            mockedServices.Add(typeof(TMock), mockedObject);
            return this;
        }

        public ServerFactory AddMock<TMock>() where TMock : class
        {
            mockedServices.Add(typeof(TMock), Mock.Of<TMock>());
            return this;
        }


        public TestServer Create()
        {
            var webHostBuilder = new WebHostBuilder()
                                    .ConfigureServices(p => p.AddSingleton(mockedServices))
                                    .UseStartup<TestStartup>();

            return new TestServer(webHostBuilder);
        }
    }
}
