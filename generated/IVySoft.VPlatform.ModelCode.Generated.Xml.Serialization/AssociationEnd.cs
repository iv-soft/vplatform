
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.ModelCode.Generated.Xml.Serialization
{
    [XmlRoot(Namespace("IVySoft.VPlatform.ModelCode.Generated"))]
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
		var result = new IVySoft.VPlatform.ModelCode.Generated.AssociationEnd();
		this.InitModel(result);
		return result;
	}

	protected void InitModel(IVySoft.VPlatform.ModelCode.Generated.AssociationEnd result)
	{

		result.Type = this.Type;
		result.Property = this.Property;
		result.Multiplicity = this.Multiplicity;
					}
    }
}
