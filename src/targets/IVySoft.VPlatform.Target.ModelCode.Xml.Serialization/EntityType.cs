using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.Target.ModelCode.Xml.Serialization
{
    [XmlRoot(Namespace = XmlConfig.Namespace)]
    public class EntityType
    {
		[XmlElement()]
		public string Name { get; set; }

        [XmlArray()]
		public Property[] Properties { get; set; }

		public IVySoft.VPlatform.Target.ModelCode.EntityType ToModel()
        {
            return new IVySoft.VPlatform.Target.ModelCode.EntityType
            {
					Name = this.Name,
				Properties = new List<IVySoft.VPlatform.Target.ModelCode.Property>(this.Properties.Select(x => x.ToModel())),
            };
        }

    }
}