using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VehicleTracking.Domain.DTO
{
    public class DeviceLoginDto
    {
        [Required]
        public string DeviceNo { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
