using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Test.Domain.Entities
{
    [Table("tbl_device")]
    public class Device : BaseEntity
    {        
        [Column("device_no", TypeName = "varchar(50)")]
        public string DeviceNo { get; set; }
        [Column("device_name", TypeName = "varchar(50)")]
        public string DeviceName { get; set; }
        [Column("password", TypeName = "varchar(50)")]
        public string Password { get; set; }
        [ForeignKey("RoleMapping"), Column("role_uuid")]
        public Guid RoleUUID { get; set; }
        public Role RoleMapping { get; set; }
    }
}
