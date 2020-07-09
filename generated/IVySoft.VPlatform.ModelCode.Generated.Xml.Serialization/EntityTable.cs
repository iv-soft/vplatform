
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.ModelCode.Generated.Xml.Serialization
{
    [XmlRoot(Namespace("IVySoft.VPlatform.ModelCode.Generated"))]
    public class EntityTable
    {
		[XmlElement()]
	        public string Name { get; set; }
		[XmlElement()]
	        public string EntityType { get; set; }

		
	public virtual object ToModel()
	{
		var result = new IVySoft.VPlatform.ModelCode.Generated.EntityTable();
		this.InitModel(result);
		return result;
	}

	protected void InitModel(IVySoft.VPlatform.ModelCode.Generated.EntityTable result)
	{

		result.Name = this.Name;
		result.EntityType = this.EntityType;
					}
    }
}
