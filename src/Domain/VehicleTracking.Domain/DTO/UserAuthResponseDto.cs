using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Domain.DTO
{
    public class UserAuthResponseDto
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
    }
}
