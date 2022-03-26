using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Test.Domain.Entities
{
    [Table("tbl_device_log")]
    public class DeviceLog : BaseEntity
    {
        [ForeignKey("DeviceMapping"), Column("device_uuid")]
        public Guid DeviceUUID { get; set; }
        [Column("logitude", TypeName = "decimal(11,8)")]
        public decimal Logitute { get; set; }
        [Column("latitude", TypeName = "decimal(10,8)")]
        public decimal Latitute { get; set; }
        [Column("fuel")]
        public double? Fuel { get; set; }
        [Column("speed")]
        public double? Speed { get; set; }
        public Device DeviceMapping { get; set; }
    }
}
