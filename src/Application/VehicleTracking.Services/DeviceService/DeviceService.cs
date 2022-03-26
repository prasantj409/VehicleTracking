using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entities;
using Test.Repository.RepositoryInerface;
using VehicleTracking.Domain.DTO;
using VehicleTracking.Domain.Response;
using VehicleTracking.Service.Const;
using VehicleTracking.Service.Encryption;
using VehicleTracking.Service.Exceptions;

namespace VehicleTracking.Service.DeviceService
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IDeviceLogRepository _deviceLogRepository;
        private readonly IRoleRepository _roleRepository;

        public DeviceService(IDeviceRepository deviceRepository,
            IDeviceLogRepository deviceLogRepository,
            IRoleRepository roleRepository)
        {
            _deviceRepository = deviceRepository;
            _deviceLogRepository = deviceLogRepository;
            _roleRepository = roleRepository;
        }

        public async Task<Device> RegisterDevice(RegisterDeviceDTO dto)
        {
            var role = await _roleRepository.SingleOrDefaultAsync(x => x.Name == AppConst.RoleType.device.ToString());

            var existingDevice = await _deviceRepository.SingleOrDefaultAsync(x => x.DeviceNo == dto.DeviceNo);
            if (existingDevice != null)
            {
                throw new BadRequestException($"DeviceNo {dto.DeviceNo} already exist.");
            }
            Device device = new Device
            {
                DeviceNo = dto.DeviceNo,
                DeviceName = dto.DeviceName,
                RoleUUID = role.UUID,
                CreatedAt = DateTime.Now,
                CreatedBy = dto.DeviceNo,
                Password = Utility.EncryptString(dto.Password)
            };

            await _deviceRepository.AddAsync(device);
            return device;
        }

        public async Task<Device> AuthenticateDevice(DeviceLoginDto dto)
        {
            var device = await _deviceRepository.GetDevice(dto.DeviceNo);            

            if (device != null)
            {
                string password = Utility.DecryptString(device.Password);
                if (password != dto.Password)
                    return null;
            }
            return device;
        }

        public async Task LogDevice(string deviceNo, DeviceLogDto dto)
        {
            var device = await _deviceRepository.SingleOrDefaultAsync(x => x.DeviceNo == deviceNo);

            if (device == null)
                throw new Exception();

            DeviceLog deviceLog = new DeviceLog
            {
                DeviceUUID = device.UUID,
                Latitute = dto.Latitude,
                Logitute = dto.Longitude,
                Fuel = dto.Fuel,
                Speed = dto.Speed,
                CreatedAt = DateTime.Now,
                CreatedBy = device.DeviceNo
            };

            await _deviceLogRepository.AddAsync(deviceLog);

        }

        public async Task<GenericResponse<IEnumerable<DeviceDto>>> GetAllDevices()
        {
            GenericResponse<IEnumerable<DeviceDto>> response;
            var deviceList = await _deviceRepository.GetAllAsync();

            var devices = deviceList.Select(x => new DeviceDto
            {
                DeviceNo = x.DeviceNo,
                DeviceName = x.DeviceName
            });

            response = new GenericResponse<IEnumerable<DeviceDto>>(devices);
            return response;
        }
            
            
            
     }
}
