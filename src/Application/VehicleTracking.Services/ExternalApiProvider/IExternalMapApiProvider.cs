using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Service.ExternalApiProvider
{
    public interface IExternalMapApiProvider
    {
        Task<string> GetLocation(decimal latitute, decimal logitute);
    }
}
