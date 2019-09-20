using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.Target.ModelCode.Xml.Serialization
{
    [XmlRoot(Namespace = XmlConfig.Namespace)]
    public class Property
    {
		[XmlElement()]
		public string Name { get; set; }
		[XmlElement()]
		public string Type { get; set; }
		[XmlElement()]
		public string Multiplicity { get; set; }
		[XmlElement()]
		public string Default { get; set; }


		public IVySoft.VPlatform.Target.ModelCode.Property ToModel()
        {
            return new IVySoft.VPlatform.Target.ModelCode.Property
            {
					Name = this.Name,
					Type = this.Type,
					Multiplicity = this.Multiplicity,
					Default = this.Default,
		            };
        }

    }
}