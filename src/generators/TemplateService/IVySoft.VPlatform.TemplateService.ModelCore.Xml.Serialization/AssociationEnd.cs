
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization
{
    [XmlRoot(Namespace = "IVySoft.VPlatform.TemplateService.ModelCore")]
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
		var result = new IVySoft.VPlatform.TemplateService.ModelCore.AssociationEnd();
		this.InitModel(result);
		return result;
	}

	protected void InitModel(IVySoft.VPlatform.TemplateService.ModelCore.AssociationEnd result)
	{

		result.Type = this.Type;
		result.Property = this.Property;
		result.Multiplicity = this.Multiplicity;
					}
    }
}
