using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Domain.DTO;
using VehicleTracking.Domain.Entities.Query;
using VehicleTracking.Domain.Response;

namespace VehicleTracking.Service.VehicleTrackingService
{
    public interface IVehicleTrackingService
    {
        Task<GenericResponse<DevicePositionDto>> GetVehicleCurrentPosition(string deviceNo);
        Task<GenericResponse<IEnumerable<DevicePositionDto>>> GetVehiclePositions(string deviceNo, VehicleQuery query);
    }
}
