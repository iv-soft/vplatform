
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization
{
    [XmlRoot(Namespace = "IVySoft.VPlatform.TemplateService.ModelCore")]
    public class Association
    {
		[XmlElement()]
	        public string Name { get; set; }
		[XmlElement()]
	        public IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization.AssociationEnd Left { get; set; }
		[XmlElement()]
	        public IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization.AssociationEnd Right { get; set; }

	
	public virtual object ToModel()
	{
		var result = new IVySoft.VPlatform.TemplateService.ModelCore.Association();
		this.InitModel(result);
		return result;
	}

	protected void InitModel(IVySoft.VPlatform.TemplateService.ModelCore.Association result)
	{

		result.Name = this.Name;
		result.Left = (IVySoft.VPlatform.TemplateService.ModelCore.AssociationEnd)this.Left?.ToModel();
		result.Right = (IVySoft.VPlatform.TemplateService.ModelCore.AssociationEnd)this.Right?.ToModel();
					}
    }
}
