using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.RabbitMQ.Message
{
    public class RecordDevice
    {
        public string DeviceNo { get; set; }        
        public decimal Logitute { get; set; }
        public decimal Latitute { get; set; }
        public double? Fuel { get; set; }
        public double? Speed { get; set; }
    }
}
