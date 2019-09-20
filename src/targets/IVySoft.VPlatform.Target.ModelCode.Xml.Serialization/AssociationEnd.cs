using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.Target.ModelCode.Xml.Serialization
{
    [XmlRoot(Namespace = XmlConfig.Namespace)]
    public class AssociationEnd
    {
		[XmlElement()]
		public string Type { get; set; }
		[XmlElement()]
		public string Property { get; set; }
		[XmlElement()]
		public string Multiplicity { get; set; }



		public IVySoft.VPlatform.Target.ModelCode.AssociationEnd ToModel()
        {
            return new IVySoft.VPlatform.Target.ModelCode.AssociationEnd
            {
				Type = this.Type,
				Property = this.Property,
				Multiplicity = this.Multiplicity,
				            };
        }

    }
}