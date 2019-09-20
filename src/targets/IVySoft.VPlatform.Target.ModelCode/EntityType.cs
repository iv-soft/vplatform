using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVySoft.VPlatform.Target.ModelCode
{
    public class EntityType
    {
	    [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

		public string Name { get; set; } 
		public string BaseType { get; set; } 
		public string Kind { get; set; } 
		public string FullName { get; set; } 

		[InverseProperty(nameof(Property.OwnerType))]
		public virtual IList<Property> Properties { get; set; }

		[ForeignKey(nameof(Module))]
		public int ModuleId { get; set; }
		public virtual Module Module { get; set; }
    }
}