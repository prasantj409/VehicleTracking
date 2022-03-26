using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTracking.API.RabbitMQ
{
    public interface IRabbitMQPublisher
    {
        Task Publish<T>(T message) where T : class;
    }
}
