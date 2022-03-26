using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Service.ExternalApiProvider
{
    public class MapOption
    {
        public string URL { get; set; }
        public string ApiKey { get; set; }
        public bool Enabled { get; set; }
    }
}
