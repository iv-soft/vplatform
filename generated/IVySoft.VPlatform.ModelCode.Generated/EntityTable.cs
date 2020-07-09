using System;
using System.ComponentModel.DataAnnotations;

namespace IVySoft.VPlatform.ModelCode.Generated
{
    public class EntityTable
    {
        [Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

	        public string Name { get; set; }
	        public string EntityType { get; set; }

	    }
}
