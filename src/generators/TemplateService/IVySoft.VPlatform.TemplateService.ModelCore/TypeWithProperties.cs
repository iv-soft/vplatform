using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVySoft.VPlatform.TemplateService.ModelCore
{
    public abstract class TypeWithProperties : IVySoft.VPlatform.TemplateService.ModelCore.ModuleType
    {

	        public bool Abstract { get; set; }

		[InverseProperty(nameof(IVySoft.VPlatform.TemplateService.ModelCore.Property.OwnerType))]
		public virtual IList<IVySoft.VPlatform.TemplateService.ModelCore.Property> Properties { get; set; }
	    }
}
