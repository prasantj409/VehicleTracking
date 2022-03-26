using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VehicleTracking.Domain.DTO
{
    public class RegisterDeviceDTO
    {
        [Required]
        public string DeviceNo { get; set; }
        public string DeviceName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
