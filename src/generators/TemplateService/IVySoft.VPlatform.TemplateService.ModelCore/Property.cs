using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVySoft.VPlatform.TemplateService.ModelCore
{
    public class Property
    {
        [Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

	        public string Name { get; set; }
	        public string Type { get; set; }
	        public string Multiplicity { get; set; }
	        public string Default { get; set; }

			[ForeignKey(nameof(OwnerType))]
		public int OwnerTypeId { get; set; }
		public virtual IVySoft.VPlatform.TemplateService.ModelCore.TypeWithProperties OwnerType { get; set; }
    }
}
