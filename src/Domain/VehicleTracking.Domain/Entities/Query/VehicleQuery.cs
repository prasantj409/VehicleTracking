using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VehicleTracking.Domain.Entities.Query
{
    public class VehicleQuery : Query
    {
        public VehicleQuery() : base(1,10)
        {

        }

        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
    }
}
