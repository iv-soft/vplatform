
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization
{
    [XmlRoot(Namespace = "IVySoft.VPlatform.TemplateService.ModelCore")]
    public class ModuleDependency
    {
		[XmlElement()]
	        public string Name { get; set; }

	
	public virtual object ToModel()
	{
		var result = new IVySoft.VPlatform.TemplateService.ModelCore.ModuleDependency();
		this.InitModel(result);
		return result;
	}

	protected void InitModel(IVySoft.VPlatform.TemplateService.ModelCore.ModuleDependency result)
	{

		result.Name = this.Name;
					}
    }
}
