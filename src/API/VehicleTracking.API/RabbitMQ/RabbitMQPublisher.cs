using MassTransit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace VehicleTracking.API.RabbitMQ
{
    public class RabbitMQPublisher : IRabbitMQPublisher
	{
		private readonly IPublishEndpoint _publishEndpoint;
		private readonly IConfiguration _configuration;
		public RabbitMQPublisher(IPublishEndpoint publishEndpoint, IConfiguration configuration)
        {
			_publishEndpoint = publishEndpoint;
			_configuration = configuration;
		}
		public async Task Publish<T>(T message) where T : class
		{
			string strConnectionTimeout = _configuration.GetSection("RabbitMQ:ConnectionTimeout").Value;
			int milliseconds = Convert.ToInt32(strConnectionTimeout == null ? "1000" : strConnectionTimeout);
			try
			{
				await _publishEndpoint.Publish(message, new CancellationTokenSource(milliseconds).Token);
			}
			catch (Exception ex)
			{
				
				//_logger.LogError($"Something went wrong: {ex}");
			}
		}
	}
}
