using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracking.Domain.DTO;
using VehicleTracking.RabbitMQ.Message;
using VehicleTracking.Service.DeviceService;

namespace VehicleTracking.Services.RecordDevicePosition.Consumer
{
    public class DeviceRecordConsumer : IConsumer<RecordDevice>
    {
        private readonly IDeviceService _deviceService;
        public DeviceRecordConsumer(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }
        public async Task Consume(ConsumeContext<RecordDevice> context)
        {
            try
            {
                DeviceLogDto dto = new DeviceLogDto
                {                
                    Latitude = context.Message.Latitute,
                    Longitude = context.Message.Logitute,
                    Fuel=context.Message.Fuel,
                    Speed = context.Message.Speed
                };

                await _deviceService.LogDevice(context.Message.DeviceNo, dto);
            }
            catch (Exception)
            {               
            }
           
        }
    }
}
