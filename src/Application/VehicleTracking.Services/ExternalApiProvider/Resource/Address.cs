using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Service.ExternalApiProvider.Resource
{
    public class Address
    {
        public string adminDistrict { get; set; }
        public string adminDistrict2 { get; set; }
        public string countryRegion { get; set; }
        public string formattedAddress { get; set; }
        public string locality { get; set; }
    }
}
