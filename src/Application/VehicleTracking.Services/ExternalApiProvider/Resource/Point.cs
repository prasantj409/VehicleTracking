using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Service.ExternalApiProvider.Resource
{
    public class Point
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }
}
