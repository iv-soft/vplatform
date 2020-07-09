
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.ModelCode.Generated.Xml.Serialization
{
    [XmlRoot(Namespace("IVySoft.VPlatform.ModelCode.Generated"))]
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
		var result = new IVySoft.VPlatform.ModelCode.Generated.ModuleType();
		this.InitModel(result);
		return result;
	}

	protected void InitModel(IVySoft.VPlatform.ModelCode.Generated.ModuleType result)
	{

		result.Name = this.Name;
		result.BaseType = this.BaseType;
		result.Discriminator = this.Discriminator;
		result.ElementName = this.ElementName;
					}
    }
}
