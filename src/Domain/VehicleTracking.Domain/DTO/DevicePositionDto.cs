using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Domain.DTO
{
    public class DevicePositionDto
    {
        public string DeviceNo { get; set; }
        public string DeviceName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Location { get; set; }
        public double? Fuel { get; set; }
        public double? Speed { get; set; }       
        public DateTime TimeStamp { get; set; }
    }
}
