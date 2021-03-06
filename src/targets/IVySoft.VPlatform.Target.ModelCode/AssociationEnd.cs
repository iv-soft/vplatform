using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVySoft.VPlatform.Target.ModelCode
{
    public class AssociationEnd
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Type { get; set; } 
		[ForeignKey(nameof(ResolvedType))]
		public int ResolvedTypeId { get; set; }
		public virtual IVySoft.VPlatform.Target.ModelCode.ModuleType ResolvedType { get; set; }
		public string Property { get; set; } 
		public string Multiplicity { get; set; } 


    }
}