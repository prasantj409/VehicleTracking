using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.API.Controllers;
using VehicleTracking.API.Test.TestRepository;
using VehicleTracking.Domain.DTO;
using VehicleTracking.Domain.RabbitMQ;
using VehicleTracking.Domain.Response;
using VehicleTracking.Service.DeviceService;
using VehicleTracking.Service.Exceptions;

namespace VehicleTracking.API.Test.UnitTest
{
    public class DeviceTest
    {
        DeviceController _controller;
        IDeviceService _deviceService;
        TestRepositoriesSinglton _db = TestRepositoriesSinglton.GetInstance();
        [SetUp]
        public void Setup()
        {
            IOptions<RabbitMQOptions> rabbitMQOptions = Options.Create<RabbitMQOptions>(new RabbitMQOptions { Enabled = false });
            _deviceService = new DeviceService(_db._deviceRepository, _db._deviceLogRepository, _db._roleRepository);
            _controller = new DeviceController(_deviceService, null, rabbitMQOptions);
        }

        [Test]
        public async Task Test1_RegisterDevice()
        {
            RegisterDeviceDTO dto = new RegisterDeviceDTO
            {
                DeviceName = "Hyundai",
                DeviceNo = "Device123",
                Password = "1234"
            };

            IActionResult result = await _controller.RegisterDevice(dto);

            var OkResult = result as OkObjectResult;
            
            var response = (GenericResponse<DeviceAuthResponseDto>)OkResult.Value;
            
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Result.DeviceNo == dto.DeviceNo);
            Assert.IsTrue(response.Result.AccessToken.Length > 0);
        }

        [Test]
        public async Task Test2_LoginDevice()
        {
            DeviceLoginDto dto = new DeviceLoginDto
            {
                DeviceNo = "Device123",
                Password = "1234"
            };
            IActionResult result = await _controller.AuthenticateDevice(dto);
            var OkResult = result as OkObjectResult;
            //Positive Test
            var response = (GenericResponse<DeviceAuthResponseDto>)OkResult.Value;
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Result.DeviceNo == dto.DeviceNo);
            Assert.IsTrue(response.Result.AccessToken.Length > 0);

            //Negative Test

            DeviceLoginDto dto2 = new DeviceLoginDto
            {
                DeviceNo = "Device12",
                Password = "1234"
            };

            var notFound = Assert.ThrowsAsync<NotFoundException>(() => _controller.AuthenticateDevice(dto2));
            Assert.IsTrue(notFound.Message == "DeviceNo or password invalid");
        }

        [Test]
        public async Task Test3_RecordDevicePosition()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.Name, "Device123"),
                    new Claim(ClaimTypes.Role,"device")
                },"TestAuthentication"));
            
            _controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            DeviceLogDto dto = new DeviceLogDto 
            { 
                Latitude = 13.8m, 
                Longitude = 11.5m 
            };

            IActionResult result = await _controller.LogDevice(dto);
            var acceptedResult = result as AcceptedResult;
            var response = (GenericResponse<DeviceLogDto>)acceptedResult.Value;
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Result.Latitude == dto.Latitude);
            Assert.IsTrue(response.Result.Longitude == dto.Longitude);
        }
    }
}
