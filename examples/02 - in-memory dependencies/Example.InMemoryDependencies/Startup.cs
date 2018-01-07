using Example.InMemoryDependencies.Core;
using Example.InMemoryDependencies.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Example.InMemoryDependencies
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            RegisterDatabase(services);
            services.AddMvc();
            RegisterCoreServices(services);
            AfterAllServicesRegistered(services);
        }

        protected void RegisterDatabase(IServiceCollection services)
        {
            services.AddDbContext<MoviesContext>(options => options.UseSqlServer("connectionString"));
        }

        protected void RegisterCoreServices(IServiceCollection services)
        {
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IQueueClient, QueueClient>();
        }

        protected virtual void AfterAllServicesRegistered(IServiceCollection services)
        {
            //FOR TESTIN PURPOSES, DO NOT DELETE
        }
    }
}
