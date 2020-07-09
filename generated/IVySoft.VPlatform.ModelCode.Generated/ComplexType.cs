using System;
using System.ComponentModel.DataAnnotations;

namespace IVySoft.VPlatform.ModelCode.Generated
{
    public class ComplexType : IVySoft.VPlatform.ModelCode.Generated.ModuleType
    {
        [Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

	        public bool Abstract { get; set; }

	    }
}
