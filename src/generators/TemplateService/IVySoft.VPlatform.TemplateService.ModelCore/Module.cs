using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVySoft.VPlatform.TemplateService.ModelCore
{
    public class Module
    {
        [Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

	        public string Name { get; set; }
	        public string Namespace { get; set; }
	        public bool IsExternal { get; set; }

		[InverseProperty(nameof(IVySoft.VPlatform.TemplateService.ModelCore.Association.Module))]
		public virtual IList<IVySoft.VPlatform.TemplateService.ModelCore.Association> Associations { get; set; }
		[InverseProperty(nameof(IVySoft.VPlatform.TemplateService.ModelCore.ModuleType.Module))]
		public virtual IList<IVySoft.VPlatform.TemplateService.ModelCore.ModuleType> Types { get; set; }
		[InverseProperty(nameof(IVySoft.VPlatform.TemplateService.ModelCore.ModuleDependency.Module))]
		public virtual IList<IVySoft.VPlatform.TemplateService.ModelCore.ModuleDependency> Dependencies { get; set; }
		[InverseProperty(nameof(IVySoft.VPlatform.TemplateService.ModelCore.EntityTable.Module))]
		public virtual IList<IVySoft.VPlatform.TemplateService.ModelCore.EntityTable> Tables { get; set; }
	    }
}
