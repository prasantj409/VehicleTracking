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
using VehicleTracking.Domain.Entities.Query;
using VehicleTracking.Domain.RabbitMQ;
using VehicleTracking.Domain.Response;
using VehicleTracking.Service.DeviceService;
using VehicleTracking.Service.Exceptions;
using VehicleTracking.Service.ExternalApiProvider;
using VehicleTracking.Service.VehicleTrackingService;

namespace VehicleTracking.API.Test.UnitTest
{
    public class VehicleTrackingTest
    {
        DeviceController _devicecontroller;
        VehicleTrackingController _controller;
        IVehicleTrackingService _trackingService;
        IDeviceService _deviceService;
        TestRepositoriesSinglton _db = TestRepositoriesSinglton.GetInstance();

        [SetUp]
        public void Setup()
        {
            var _mapProvider = new Mock<IExternalMapApiProvider>();
            _mapProvider.Setup(s => s.GetLocation(It.IsAny<decimal>(), It.IsAny<decimal>())).ReturnsAsync("TestLocation");

            _deviceService = new DeviceService(_db._deviceRepository, _db._deviceLogRepository, _db._roleRepository);
            IOptions<MapOption> mapOption = Options.Create< MapOption>(new MapOption { Enabled = true });
            IOptions<RabbitMQOptions> rabbitMQOptions = Options.Create<RabbitMQOptions>(new RabbitMQOptions { Enabled = false });

            _trackingService = new VehicleTrackingService(_db._deviceLogRepository, _db._deviceRepository, _mapProvider.Object, mapOption);
            _controller = new VehicleTrackingController(_trackingService, _deviceService);
           
            _devicecontroller = new DeviceController(_deviceService, null, rabbitMQOptions);
        }

        [Test]
        public async Task Test1_VehicleCurrentPosition()
        {
            /////////Register Device
            RegisterDeviceDTO dto = new RegisterDeviceDTO
            {
                DeviceName = "Toyota",
                DeviceNo = "TestDevice",
                Password = "1234"
            };

            IActionResult result = await _devicecontroller.RegisterDevice(dto);
            var OkResult = result as OkObjectResult;
            var response = (GenericResponse<DeviceAuthResponseDto>)OkResult.Value;

            // When no data available
            var notFound = Assert.ThrowsAsync<NotFoundException>(() => _controller.GetCurrentPosition(response.Result.DeviceNo));
            Assert.IsTrue(notFound.Message == "Device Positions not available");
            

            //////////Record Position
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.Name, response.Result.DeviceNo),
                    new Claim(ClaimTypes.Role,"device")
                }, "TestAuthentication"));

            DeviceLogDto devicePositionDto = new DeviceLogDto
            {
                Latitude = 13.8m,
                Longitude = 11.5m
            };

            _devicecontroller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            IActionResult result2 = await _devicecontroller.LogDevice(devicePositionDto);
            var acceptedResult = result2 as AcceptedResult;
            var response2 = (GenericResponse<DeviceLogDto>)acceptedResult.Value;

            ///// Get Current Position Test

            IActionResult currentPositionResult = await _controller.GetCurrentPosition(response.Result.DeviceNo);
            var okResult = currentPositionResult as OkObjectResult;
            var currentPositionResponse = (GenericResponse<DevicePositionDto>)okResult.Value;
            Assert.IsTrue(currentPositionResponse.Result.DeviceNo == response.Result.DeviceNo);
            Assert.IsTrue(currentPositionResponse.Result.Latitude == devicePositionDto.Latitude);
            Assert.IsTrue(currentPositionResponse.Result.Longitude == devicePositionDto.Longitude);
            Assert.IsTrue(currentPositionResponse.Result.Location == "TestLocation");

            //Negative Test
            var notFound2 = Assert.ThrowsAsync<NotFoundException>(() => _controller.GetCurrentPosition("device"));
            Assert.IsTrue(notFound2.Message == "Device device not found");


        }

        [Test]
        public async Task Test2_VehiclePositionByTime()
        {
            //////////Record Position
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.Name, "TestDevice"),
                    new Claim(ClaimTypes.Role,"device")
                }, "TestAuthentication"));
            _devicecontroller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            for (int i = 1; i < 20; i++)
            {
                DeviceLogDto devicePositionDto = new DeviceLogDto
                {
                    Latitude = 13.8m+i,
                    Longitude = 11.5m+i
                };

                
                IActionResult result2 = await _devicecontroller.LogDevice(devicePositionDto);
                var acceptedResult = result2 as AcceptedResult;
                var response2 = (GenericResponse<DeviceLogDto>)acceptedResult.Value;
            }

            ///// Get Device Positions Test
            VehicleQuery query = new VehicleQuery
            {
                StartTime = DateTime.Now.AddMinutes(-3),
                EndTime = DateTime.Now,
                page = 1,
                limit = 10
            };


            IActionResult currentPositionResult = await _controller.GetPosition("TestDevice", query);
            var okResult = currentPositionResult as OkObjectResult;
            var response = (GenericResponse<IEnumerable<DevicePositionDto>>)okResult.Value;
            Assert.IsTrue(response.Result.Count() == 10);
            Assert.IsTrue(response.Result.All(x=>x.DeviceNo == "TestDevice"));
            Assert.IsTrue(response.Result.All(x=>x.TimeStamp > query.StartTime));
            Assert.IsTrue(response.Result.All(x => x.TimeStamp < query.EndTime));


        }
    }
}
