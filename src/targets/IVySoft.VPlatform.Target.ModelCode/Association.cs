using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVySoft.VPlatform.Target.ModelCode
{
    public class Association
    {
	    [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

		public string Name { get; set; } 
		[ForeignKey(nameof(Left))]
		public int LeftId { get; set; }
		public virtual IVySoft.VPlatform.Target.ModelCode.AssociationEnd Left { get; set; }
		[ForeignKey(nameof(Right))]
		public int RightId { get; set; }
		public virtual IVySoft.VPlatform.Target.ModelCode.AssociationEnd Right { get; set; }


		[ForeignKey(nameof(Module))]
		public int ModuleId { get; set; }
		public virtual Module Module { get; set; }
    }
}