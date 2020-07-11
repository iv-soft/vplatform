
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization
{
    [XmlRoot(Namespace = "IVySoft.VPlatform.TemplateService.ModelCore")]
    public class EntityType : IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization.TypeWithProperties
    {
		[XmlElement()]
	        public bool Abstract { get; set; }

	
	public override object ToModel()
	{
		var result = new IVySoft.VPlatform.TemplateService.ModelCore.EntityType();
		this.InitModel(result);
		return result;
	}

	protected void InitModel(IVySoft.VPlatform.TemplateService.ModelCore.EntityType result)
	{
		base.InitModel(result);

		result.Abstract = this.Abstract;
					}
    }
}
