using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Test.Domain.Entities
{
    [Table("tbl_role")]
    public class Role : BaseEntity
    {
        [Column("name", TypeName = "varchar(50)")]
        public string Name { get; set; }
    }
}
