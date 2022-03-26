using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.DB;

namespace TestAPI
{
    public class Program
    {
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();
			using (var scope = host.Services.CreateScope())
			{
				//var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
				try
				{
					var concreteContext = scope.ServiceProvider.GetService<DataContext>();					
					Policy
					  .Handle<Exception>()
					  .WaitAndRetry(5, r => TimeSpan.FromSeconds(10))
					  .Execute(() => concreteContext.Database.Migrate());
					//logger.Debug("init main");

				}
				catch (Exception exception)
				{
					//NLog: catch setup errors
					//logger.Error(exception, "Stopped program because of exception");
					throw;
				}
				finally
				{
					host.Run();
					// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
					//NLog.LogManager.Shutdown();
				}

			}
		}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
