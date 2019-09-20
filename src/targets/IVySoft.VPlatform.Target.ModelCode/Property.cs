using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVySoft.VPlatform.Target.ModelCode
{
    public class Property
    {
	    [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

		public string Name { get; set; } 
		public string Type { get; set; } 
		[ForeignKey(nameof(ResolvedType))]
		public int? ResolvedTypeId { get; set; }
		public virtual IVySoft.VPlatform.Target.ModelCode.EntityType ResolvedType { get; set; }
		public bool Nullable { get; set; } 
		public string Default { get; set; } 


		[ForeignKey(nameof(OwnerType))]
		public int OwnerTypeId { get; set; }
		public virtual EntityType OwnerType { get; set; }
    }
}