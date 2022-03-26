using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Test.Domain.Entities
{
    public class BaseEntity
    {
		//public int Id { get; set; }
		//[MaxLength(50), Required, Column("uuid", TypeName = "varchar(50)")]
		[Column("uuid")]
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid UUID { get; set; }
		[Column("created_at")]
		public DateTime CreatedAt { get; set; }
		[MaxLength(50), Column("created_by", TypeName = "varchar(50)")]
		public string CreatedBy { get; set; }
		[Column("edited_at")]
		public DateTime? EditedAt { get; set; }
		[MaxLength(50), Column("edited_by", TypeName = "varchar(50)")]
		public string EditedBy { get; set; }
	}
}
