using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVySoft.VPlatform.TemplateService.ModelCore
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

			[ForeignKey(nameof(Module))]
		public int ModuleId { get; set; }
		public virtual IVySoft.VPlatform.TemplateService.ModelCore.Module Module { get; set; }
    }
}
