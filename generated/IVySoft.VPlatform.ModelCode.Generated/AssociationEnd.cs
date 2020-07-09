using System;
using System.ComponentModel.DataAnnotations;

namespace IVySoft.VPlatform.ModelCode.Generated
{
    public class AssociationEnd
    {
        [Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

	        public string Type { get; set; }
	        public string Property { get; set; }
	        public string Multiplicity { get; set; }

	    }
}
