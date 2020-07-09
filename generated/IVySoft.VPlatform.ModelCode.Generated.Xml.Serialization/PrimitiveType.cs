
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.ModelCode.Generated.Xml.Serialization
{
    [XmlRoot(Namespace("IVySoft.VPlatform.ModelCode.Generated"))]
    public class PrimitiveType : IVySoft.VPlatform.ModelCode.Generated.ModuleType
    {

		
	public override object ToModel()
	{
		var result = new IVySoft.VPlatform.ModelCode.Generated.PrimitiveType();
		this.InitModel(result);
		return result;
	}

	protected void InitModel(IVySoft.VPlatform.ModelCode.Generated.PrimitiveType result)
	{
		base.InitModel(result);

					}
    }
}
