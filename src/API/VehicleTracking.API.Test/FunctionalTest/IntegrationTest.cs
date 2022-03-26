using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Test.DB;
using TestAPI;
using VehicleTracking.API.RabbitMQ;
using Microsoft.Extensions.Configuration;

namespace VehicleTracking.API.Test.FunctionalTest
{
    public class IntegrationTest
    {
        protected readonly HttpClient _client;
        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
				builder.ConfigureAppConfiguration(config =>
				{
					var settings = new Dictionary<string, string>
						{
							{"RabbitMQ:Enabled", "false"},
							{"MapProvider:Enabled", "false"}

						};
					config.AddInMemoryCollection(settings);
				});

				builder.ConfigureServices(services =>
				{
					var descriptor = services.SingleOrDefault(
						d => d.ServiceType ==
							typeof(DbContextOptions<DataContext>));
					services.Remove(descriptor);

					string dataPath = Path.Combine(Directory.GetCurrentDirectory(), "vehicle_tracking_functional_test.db");
					if (File.Exists(dataPath))
					{
						File.Delete(dataPath);
					}
					services.AddDbContext<DataContext>(options => { options.UseSqlite("Filename=./vehicle_tracking_functional_test.db"); });
					var context = services.BuildServiceProvider().GetService<DataContext>();
					context.Database.OpenConnection();
					context.Database.EnsureCreated();
					
					var descriptor1 = services.SingleOrDefault(
						d => d.ServiceType.Name == "IRabbitMQPublisher");
					services.Remove(descriptor1);
					services.AddScoped<IRabbitMQPublisher, TestRabbitMqPublisher>();


				});
			});

            _client = appFactory.CreateClient();
        }
    }

    public class TestRabbitMqPublisher : IRabbitMQPublisher
    {
        public Task Publish<T>(T message) where T : class
        {
			return Task.CompletedTask;
        }
    }
}
