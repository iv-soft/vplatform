
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization
{
    [XmlRoot(Namespace = "IVySoft.VPlatform.TemplateService.ModelCore")]
    public class ModuleType
    {
		[XmlElement()]
	        public string Name { get; set; }
		[XmlElement()]
	        public string BaseType { get; set; }
		[XmlElement()]
	        public string Discriminator { get; set; }
		[XmlElement()]
	        public string ElementName { get; set; }

	
	public virtual object ToModel()
	{
		var result = new IVySoft.VPlatform.TemplateService.ModelCore.ModuleType();
		this.InitModel(result);
		return result;
	}

	protected void InitModel(IVySoft.VPlatform.TemplateService.ModelCore.ModuleType result)
	{

		result.Name = this.Name;
		result.BaseType = this.BaseType;
		result.Discriminator = this.Discriminator;
		result.ElementName = this.ElementName;
					}
    }
}
