using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVySoft.VPlatform.Target.ModelCode
{
    public class EntityType : IVySoft.VPlatform.Target.ModelCode.ModuleType
    {

		public bool Abstract { get; set; } 

		[InverseProperty(nameof(Property.OwnerType))]
		public virtual IList<Property> Properties { get; set; }

    }
}