using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.Target.ModelCode.Xml.Serialization
{
    [XmlRoot(Namespace = XmlConfig.Namespace)]
    public class Association
    {
		[XmlElement()]
		public string Name { get; set; }
		[XmlElement()]
		public AssociationEnd Left { get; set; }
		[XmlElement()]
		public AssociationEnd Right { get; set; }



		public IVySoft.VPlatform.Target.ModelCode.Association ToModel()
        {
            return new IVySoft.VPlatform.Target.ModelCode.Association
            {
				Name = this.Name,
				Left = this.Left?.ToModel(),
				Right = this.Right?.ToModel(),
				            };
        }

    }
}