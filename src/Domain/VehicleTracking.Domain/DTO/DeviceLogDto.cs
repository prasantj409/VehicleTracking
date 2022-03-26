using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VehicleTracking.Domain.DTO
{
    public class DeviceLogDto
    {
        [Required]
        public decimal Latitude { get; set; }
        [Required]
        public decimal Longitude { get; set; }        
        public double? Fuel { get; set; }
        public double? Speed { get; set; }
    }
}
