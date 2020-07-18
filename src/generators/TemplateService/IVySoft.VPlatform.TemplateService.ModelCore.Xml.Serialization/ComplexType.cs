
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization
{
    [XmlRoot(Namespace = "IVySoft.VPlatform.TemplateService.ModelCore")]
    public class ComplexType : IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization.TypeWithProperties
    {

	
	public override object ToModel()
	{
		var result = new IVySoft.VPlatform.TemplateService.ModelCore.ComplexType();
		this.InitModel(result);
		return result;
	}

	protected void InitModel(IVySoft.VPlatform.TemplateService.ModelCore.ComplexType result)
	{
		base.InitModel(result);

					}
    }
}
