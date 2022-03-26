using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracking.Domain.Entities.Query;
using VehicleTracking.Service.DeviceService;
using VehicleTracking.Service.VehicleTrackingService;

namespace VehicleTracking.API.Controllers
{
    [Route("api",Name ="Admin Vehicle Tracking")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "admin")]
    public class VehicleTrackingController : ControllerBase
    {
        private readonly IVehicleTrackingService _vehicleTrackingService;
        private readonly IDeviceService _deviceService;
        public VehicleTrackingController(IVehicleTrackingService vehicleTrackingService, IDeviceService deviceService)
        {
            _vehicleTrackingService = vehicleTrackingService;
            _deviceService = deviceService;
        }

        /// <summary>
        /// This end point will return all registerd devices. Need amin authorization
        /// </summary>
        /// <returns></returns>
        [HttpGet("vehicle/device")]
        public async Task<ActionResult> GetAllDevice()
        {
            var result = await _deviceService.GetAllDevices();

            return Ok(result);
        }

        /// <summary>
        /// This end point will return current position of device. Need admin authorization
        /// </summary>
        /// <param name="device_no"></param>
        /// <returns></returns>
        [HttpGet("vehicle/track/currentposition/device/{device_no}")]
        public async Task<ActionResult> GetCurrentPosition(string device_no)
        {
            var result = await _vehicleTrackingService.GetVehicleCurrentPosition(device_no);

            return Ok(result);
        }

        /// <summary>
        /// This end point will return device positions for a certain time. Need amin authorization
        /// </summary>
        /// <param name="device_no"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("vehicle/track/device/{device_no}")]
        public async Task<ActionResult> GetPosition(string device_no, [FromQuery] VehicleQuery query)
        {
            var result = await _vehicleTrackingService.GetVehiclePositions(device_no, query);

            return Ok(result);
        }
    }
}
