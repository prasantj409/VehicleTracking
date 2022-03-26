using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Domain.DTO;
using VehicleTracking.Domain.Response;

namespace VehicleTracking.API.Test.FunctionalTest
{
    public class VehicleScenarioTest : VehicleScenarioBase
    {
        [Test]
        public async Task DeviceFunctionalTest()
        {
            // Register Device
            RegisterDeviceDTO dto = new RegisterDeviceDTO
            {
                DeviceName = "Hyundai",
                DeviceNo = "Device123",
                Password = "1234"
            };

            var resourcejson = CreateJsonContent(dto);            

            var response = await _client.PostAsync(Post.RegisterDevice(), resourcejson);
            response.EnsureSuccessStatusCode();
            var deviceRegisterResponseString = await response.Content.ReadAsStringAsync();
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(deviceRegisterResponseString);
            DeviceAuthResponseDto deviceRegisterResponse = JsonConvert.DeserializeObject<DeviceAuthResponseDto>(apiResponse.Result.ToString());
            Assert.IsTrue(apiResponse.success == true);
            Assert.IsTrue(deviceRegisterResponse !=null);
            Assert.IsTrue(deviceRegisterResponse.DeviceNo== dto.DeviceNo);
            Assert.IsTrue(deviceRegisterResponse.AccessToken.Length > 0);

            //Record Device Position

            DeviceLogDto dtoPosition = new DeviceLogDto
            {
                Latitude = 13.8m,
                Longitude = 11.5m,
                Fuel=20,
                Speed=60
            };

            resourcejson = CreateJsonContent(dtoPosition);
            //Un-Authorize test
            response = await _client.PostAsync(Post.RecordDevicePosition(), resourcejson);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Unauthorized);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", deviceRegisterResponse.AccessToken);

            response = await _client.PostAsync(Post.RecordDevicePosition(), resourcejson);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonString);
            DeviceLogDto recordedDevice = JsonConvert.DeserializeObject<DeviceLogDto>(apiResponse.Result.ToString());
            Assert.IsTrue(apiResponse.success == true);
            Assert.IsTrue(recordedDevice != null);
            Assert.IsTrue(recordedDevice.Latitude == dtoPosition.Latitude);
            Assert.IsTrue(recordedDevice.Longitude== dtoPosition.Longitude);
            Assert.IsTrue(recordedDevice.Fuel == dtoPosition.Fuel);
            Assert.IsTrue(recordedDevice.Speed == dtoPosition.Speed);
           
        }

        [Test]
        public async Task VehicleTrackingFunctionalTest()
        {
            //Admin Login
            UserLoginDto user = new UserLoginDto
            {
                userName = "system",
                password = "system"
            };

            var resourcejson = CreateJsonContent(user);

            
            var response = await _client.PostAsync(Post.AdminLogin(), resourcejson);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonString);
            UserAuthResponseDto userAuth = JsonConvert.DeserializeObject<UserAuthResponseDto>(apiResponse.Result.ToString());
            Assert.IsTrue(apiResponse.success == true);
            Assert.IsTrue(userAuth != null);
            Assert.IsTrue(userAuth.UserName == "system");
            Assert.IsTrue(userAuth.AccessToken.Length>0);

            // Device Current Position Test
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAuth.AccessToken);
            response = await _client.GetAsync(Get.GetDeviceCurrentPosition("Device123"));
            response.EnsureSuccessStatusCode();
            jsonString = await response.Content.ReadAsStringAsync();
            

            apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonString);
            DevicePositionDto devicePositionDto = JsonConvert.DeserializeObject<DevicePositionDto>(apiResponse.Result.ToString());
            Assert.IsTrue(apiResponse.success == true);
            Assert.IsTrue(devicePositionDto != null);
            Assert.IsTrue(devicePositionDto.DeviceNo == "Device123");
            Assert.IsTrue(devicePositionDto.Latitude == 13.8m);
            Assert.IsTrue(devicePositionDto.Longitude == 11.5m);
            Assert.IsTrue(devicePositionDto.Fuel == 20);
            Assert.IsTrue(devicePositionDto.Speed == 60);

            //Device positions test for a certain time
            await Task.Delay(1000);
            response = await _client.GetAsync(Get.GetDevicePositions("Device123",DateTime.Now.AddMinutes(-5),DateTime.Now,1,10));
            response.EnsureSuccessStatusCode();
            jsonString = await response.Content.ReadAsStringAsync();
            apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonString);
            IEnumerable<DevicePositionDto> devicePositions = JsonConvert.DeserializeObject<IEnumerable<DevicePositionDto>>(apiResponse.Result.ToString());

            Assert.IsTrue(apiResponse.success == true);
            Assert.IsTrue(devicePositionDto != null);
            Assert.IsTrue(devicePositions.Any(x=>x.DeviceNo == "Device123"));
            Assert.IsTrue(devicePositions.Any(x => x.Latitude == 13.8m));
            Assert.IsTrue(devicePositions.Any(x => x.Longitude == 11.5m));
            Assert.IsTrue(devicePositions.Any(x => x.Fuel == 20));
            Assert.IsTrue(devicePositions.Any(x => x.Speed == 60));

        }
    }
}
