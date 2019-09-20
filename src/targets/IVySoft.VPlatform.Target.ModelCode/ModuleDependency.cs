using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVySoft.VPlatform.Target.ModelCode
{
    public class ModuleDependency
    {
	    [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

		public string Name { get; set; } 


		[ForeignKey(nameof(Module))]
		public int ModuleId { get; set; }
		public virtual Module Module { get; set; }
    }
}