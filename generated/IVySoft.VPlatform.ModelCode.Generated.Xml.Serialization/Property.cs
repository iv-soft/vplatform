
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.ModelCode.Generated.Xml.Serialization
{
    [XmlRoot(Namespace("IVySoft.VPlatform.ModelCode.Generated"))]
    public class Property
    {
		[XmlElement()]
	        public string Name { get; set; }
		[XmlElement()]
	        public string Type { get; set; }
		[XmlElement()]
	        public string Multiplicity { get; set; }
		[XmlElement()]
	        public string Default { get; set; }

		
	public virtual object ToModel()
	{
		var result = new IVySoft.VPlatform.ModelCode.Generated.Property();
		this.InitModel(result);
		return result;
	}

	protected void InitModel(IVySoft.VPlatform.ModelCode.Generated.Property result)
	{

		result.Name = this.Name;
		result.Type = this.Type;
		result.Multiplicity = this.Multiplicity;
		result.Default = this.Default;
					}
    }
}
