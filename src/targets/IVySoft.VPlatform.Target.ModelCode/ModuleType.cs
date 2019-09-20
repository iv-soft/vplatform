using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVySoft.VPlatform.Target.ModelCode
{
    public class ModuleType
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Name { get; set; } 
		public string BaseType { get; set; } 
		[ForeignKey(nameof(ResolvedBaseType))]
		public int ResolvedBaseTypeId { get; set; }
		public virtual IVySoft.VPlatform.Target.ModelCode.ModuleType ResolvedBaseType { get; set; }
		public string Discriminator { get; set; } 
		public string ElementName { get; set; } 
		public string FullName { get; set; } 


		[ForeignKey(nameof(Module))]
		public int ModuleId { get; set; }
		public virtual Module Module { get; set; }
    }
}