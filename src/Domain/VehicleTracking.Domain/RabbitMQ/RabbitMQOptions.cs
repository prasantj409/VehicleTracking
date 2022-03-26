using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Domain.RabbitMQ
{
    public class RabbitMQOptions
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Enabled { get; set; }
    }
}
