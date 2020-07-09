using System;
using System.ComponentModel.DataAnnotations;

namespace IVySoft.VPlatform.ModelCode.Generated
{
    public class ModuleType
    {
        [Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

	        public string Name { get; set; }
	        public string BaseType { get; set; }
	        public string Discriminator { get; set; }
	        public string ElementName { get; set; }

	    }
}
