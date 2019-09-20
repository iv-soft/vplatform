using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVySoft.VPlatform.Target.ModelCode
{
    public class Module
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Name { get; set; } 
		public string Namespace { get; set; } 
		public bool IsExternal { get; set; } 

		[InverseProperty(nameof(Association.Module))]
		public virtual IList<Association> Associations { get; set; }
		[InverseProperty(nameof(ModuleType.Module))]
		public virtual IList<ModuleType> Types { get; set; }
		[InverseProperty(nameof(ModuleDependency.Module))]
		public virtual IList<ModuleDependency> Dependencies { get; set; }

    }
}