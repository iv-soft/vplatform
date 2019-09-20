using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.Target.ModelCode.Xml.Serialization
{
    [XmlRoot(Namespace = XmlConfig.Namespace)]
    public class Module
    {
		[XmlElement()]
		public string Name { get; set; }
		[XmlElement()]
		public string Namespace { get; set; }

        [XmlArray()]
		public EntityType[] Types { get; set; }

		public IVySoft.VPlatform.Target.ModelCode.Module ToModel()
        {
            return new IVySoft.VPlatform.Target.ModelCode.Module
            {
					Name = this.Name,
					Namespace = this.Namespace,
				Types = new List<IVySoft.VPlatform.Target.ModelCode.EntityType>(this.Types.Select(x => x.ToModel())),
            };
        }

    }
}