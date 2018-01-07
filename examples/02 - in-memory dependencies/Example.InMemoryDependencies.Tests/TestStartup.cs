using System;
using System.Collections.Generic;
using Example.InMemoryDependencies.Core;
using Example.InMemoryDependencies.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Example.InMemoryDependencies.Tests
{
    public class TestStartup : Startup
    {
        private readonly IDictionary<Type, object> mockedServices;

        public TestStartup(IDictionary<Type, object> mockedServices)
        {
            this.mockedServices = mockedServices;
        }

        protected override void AfterAllServicesRegistered(IServiceCollection services)
        {
            base.RegisterCoreServices(services);
            foreach (var mockedService in mockedServices)
                services.AddTransient(mockedService.Key, _ => mockedService.Value);
        }
    }
}
