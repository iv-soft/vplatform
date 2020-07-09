using System;
using System.ComponentModel.DataAnnotations;

namespace IVySoft.VPlatform.ModelCode.Generated
{
    public class Association
    {
        [Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

	        public string Name { get; set; }
	        public AssociationEnd Left { get; set; }
	        public AssociationEnd Right { get; set; }

	    }
}
