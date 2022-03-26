using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Security;
using VehicleTracking.API.RabbitMQ;
using VehicleTracking.Domain.DTO;
using VehicleTracking.Domain.RabbitMQ;
using VehicleTracking.Domain.Response;
using VehicleTracking.RabbitMQ.Message;
using VehicleTracking.Service.DeviceService;
using VehicleTracking.Service.Exceptions;

namespace VehicleTracking.API.Controllers
{
    [Route("api",Name ="Device")]
    [ApiController]
    [Produces("application/json")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private readonly IRabbitMQPublisher _rabbitMqPublisher;
        private readonly RabbitMQOptions _rabbitMQOptions;
        public DeviceController(IDeviceService deviceService, IRabbitMQPublisher rabbitMqPublisher, IOptions<RabbitMQOptions> rabbitMQOptions)
        {
            _deviceService = deviceService;
            _rabbitMqPublisher = rabbitMqPublisher;
            _rabbitMQOptions = rabbitMQOptions.Value;
        }

        /// <summary>
        /// End point to Register device. Device has to enter deviceNo and password. Response will be a bearer token for authorization. We assume password is
        /// configured at device end
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("device/register")]
        public async Task<ActionResult> RegisterDevice(RegisterDeviceDTO dto)
        {
            var device = await _deviceService.RegisterDevice(dto);

            var token = DeviceTokenService.CreateToken(device);

            var authDevice = new DeviceAuthResponseDto
            {
                DeviceNo = device.DeviceNo,
                AccessToken = token
            };
            GenericResponse<DeviceAuthResponseDto> response = new GenericResponse<DeviceAuthResponseDto>(authDevice);
            return Ok(response);
        }

        /// <summary>
        /// End point to login for device. Device need to enter DeviceNo and Password. If device token is expired then device can use this end point to 
        /// re-generate bearer token
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("device/login")]
        public async Task<ActionResult> AuthenticateDevice(DeviceLoginDto dto)
        {
            var device = await _deviceService.AuthenticateDevice(dto);

            if (device == null)
                throw new NotFoundException("DeviceNo or password invalid");
            var token = DeviceTokenService.CreateToken(device);
            var authDevice = new DeviceAuthResponseDto
            {
                DeviceNo = device.DeviceNo,
                AccessToken = token
            };
            GenericResponse<DeviceAuthResponseDto> response = new GenericResponse<DeviceAuthResponseDto>(authDevice);
            return Ok(response);
        }

        /// <summary>
        /// End point to register device position. Device should be authorize to use this end point. This end point publish a message to micro service through
        /// rabbitmq to record device position. RabbitMQ setting should be enable in appsettings.json for performance. If not enabled then device record position will
        /// be handle by this API.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles ="device")]        
        [HttpPost("device/record_position")]
        [ProducesResponseType(typeof(GenericResponse<DeviceLogDto>), StatusCodes.Status202Accepted)]
        public async Task<ActionResult> LogDevice(DeviceLogDto dto )
        {
            string deviceNo = HttpContext.User.Identity.Name;
            if (_rabbitMQOptions.Enabled)
            {
                var command = new RecordDevice
                {
                    DeviceNo = deviceNo,
                    Latitute = dto.Latitude,
                    Logitute = dto.Longitude,
                    Fuel = dto.Fuel,
                    Speed = dto.Speed                    
                };

                await _rabbitMqPublisher.Publish<RecordDevice>(command);
            }
            else
                await _deviceService.LogDevice(deviceNo, dto);
            GenericResponse<DeviceLogDto> response = new GenericResponse<DeviceLogDto>(dto);
            return Accepted(response);
        }
    }
}
