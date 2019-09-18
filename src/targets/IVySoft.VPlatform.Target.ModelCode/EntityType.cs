using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVySoft.VPlatform.Target.ModelCode
{
    public class EntityType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Name { get; set; }
        public virtual IList<Property> Properties { get; set; }
    }
}