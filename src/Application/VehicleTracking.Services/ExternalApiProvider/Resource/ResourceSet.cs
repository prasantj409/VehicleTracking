using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Service.ExternalApiProvider.Resource
{
    public class ResourceSet
    {
        public int estimatedTotal { get; set; }
        public List<Resource> resources { get; set; }
    }
}
