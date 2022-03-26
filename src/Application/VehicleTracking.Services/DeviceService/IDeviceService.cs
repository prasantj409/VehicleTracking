using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entities;
using VehicleTracking.Domain.DTO;
using VehicleTracking.Domain.Response;

namespace VehicleTracking.Service.DeviceService
{
    public interface IDeviceService
    {
        Task<Device> RegisterDevice(RegisterDeviceDTO dto);
        Task<Device> AuthenticateDevice(DeviceLoginDto dto);
        Task LogDevice(string deviceNo, DeviceLogDto dto);
        Task<GenericResponse<IEnumerable<DeviceDto>>> GetAllDevices();
    }
}
