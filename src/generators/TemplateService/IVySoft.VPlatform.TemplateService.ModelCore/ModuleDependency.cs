using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVySoft.VPlatform.TemplateService.ModelCore
{
    public class ModuleDependency
    {
        [Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

	        public string Name { get; set; }

			[ForeignKey(nameof(Module))]
		public int ModuleId { get; set; }
		public virtual IVySoft.VPlatform.TemplateService.ModelCore.Module Module { get; set; }
    }
}
