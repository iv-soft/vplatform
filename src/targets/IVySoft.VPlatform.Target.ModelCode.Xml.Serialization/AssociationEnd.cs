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



		public virtual object ToModel()
		{
			var result = new IVySoft.VPlatform.Target.ModelCode.AssociationEnd();
			this.InitModel(result);
			return result;
		}

		protected void InitModel(IVySoft.VPlatform.Target.ModelCode.AssociationEnd result)
		{

				result.Type = this.Type;
				result.Property = this.Property;
				result.Multiplicity = this.Multiplicity;
				        }
    }
}