using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Repository.RepositoryInerface;
using VehicleTracking.Domain.DTO;
using VehicleTracking.Domain.Entities.Query;
using VehicleTracking.Domain.Response;
using VehicleTracking.Service.Exceptions;
using VehicleTracking.Service.ExternalApiProvider;

namespace VehicleTracking.Service.VehicleTrackingService
{
    public class VehicleTrackingService : IVehicleTrackingService
    {
        private readonly IDeviceLogRepository _deviceLogRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IExternalMapApiProvider _mapProvider;
        private readonly MapOption _mapOption;
        public VehicleTrackingService(IDeviceLogRepository deviceLogRepository, 
            IDeviceRepository deviceRepository, 
            IExternalMapApiProvider mapProvider,
             IOptions<MapOption> mapOption)
        {
            _deviceLogRepository = deviceLogRepository;
            _deviceRepository = deviceRepository;
            _mapProvider = mapProvider;
            _mapOption = mapOption.Value;
        }

        public async Task<GenericResponse<DevicePositionDto>> GetVehicleCurrentPosition(string deviceNo)
        {
            GenericResponse<DevicePositionDto> response;
            //DevicePositionDto response;
            var Device = await _deviceRepository.SingleOrDefaultAsync(x => x.DeviceNo == deviceNo);
            if (Device == null)
                throw new NotFoundException($"Device {deviceNo} not found");

            var deviceDetail = await _deviceLogRepository.GetDeviceLogByID(Device.UUID);

            if (deviceDetail == null)
            {
                throw new NotFoundException("Device Positions not available");                
            }

            var resource = new DevicePositionDto
            {
                DeviceNo = deviceDetail.DeviceMapping.DeviceNo,
                DeviceName = deviceDetail.DeviceMapping.DeviceName,
                Longitude = deviceDetail.Logitute,
                Latitude = deviceDetail.Latitute,
                TimeStamp = deviceDetail.CreatedAt,
                Fuel=deviceDetail.Fuel,
                Speed=deviceDetail.Speed
            };

            if(_mapOption.Enabled)
                resource .Location = await _mapProvider.GetLocation(resource.Latitude, resource.Longitude);

            response = new GenericResponse<DevicePositionDto>(resource);            
            return response;
        }

        public async Task<GenericResponse<IEnumerable<DevicePositionDto>>> GetVehiclePositions(string deviceNo, VehicleQuery query)
        {
            GenericResponse<IEnumerable<DevicePositionDto>> response;

            var Device = await _deviceRepository.SingleOrDefaultAsync(x => x.DeviceNo == deviceNo);
            if (Device == null)
                throw new NotFoundException($"Device {deviceNo}not found");

            var deviceDetails = await _deviceLogRepository.GetDeviceLogByTime(Device.UUID, query);

            if (deviceDetails == null)
            {
                response = new GenericResponse<IEnumerable<DevicePositionDto>>("Device Positions not awailable");
                response.success = false;
            }

            IEnumerable<DevicePositionDto> resource; 
            resource = deviceDetails.Select(x => new DevicePositionDto
            {
                DeviceName = x.DeviceMapping.DeviceName,
                DeviceNo = x.DeviceMapping.DeviceNo,
                Latitude = x.Latitute,
                Longitude = x.Logitute,
                TimeStamp = x.CreatedAt,
                Fuel = x.Fuel,
                Speed = x.Speed
            });           
                

            response = new GenericResponse<IEnumerable<DevicePositionDto>>(resource);            
            return response;
        }
    }
}
