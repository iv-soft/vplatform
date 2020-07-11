
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization
{
    [XmlRoot(Namespace = "IVySoft.VPlatform.TemplateService.ModelCore")]
    public class TypeWithProperties : IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization.ModuleType
    {

        [XmlArray()]
	public IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization.Property[] Properties { get; set; }
	

	protected void InitModel(IVySoft.VPlatform.TemplateService.ModelCore.TypeWithProperties result)
	{
		base.InitModel(result);

						result.Properties = new List<IVySoft.VPlatform.TemplateService.ModelCore.Property>((this.Properties == null) ? new IVySoft.VPlatform.TemplateService.ModelCore.Property[0] : this.Properties.Select(x => (IVySoft.VPlatform.TemplateService.ModelCore.Property)x.ToModel()));
	}
    }
}
