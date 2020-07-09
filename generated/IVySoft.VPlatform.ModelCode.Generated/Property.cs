using System;
using System.ComponentModel.DataAnnotations;

namespace IVySoft.VPlatform.ModelCode.Generated
{
    public class Property
    {
        [Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

	        public string Name { get; set; }
	        public string Type { get; set; }
	        public string Multiplicity { get; set; }
	        public string Default { get; set; }

	    }
}
