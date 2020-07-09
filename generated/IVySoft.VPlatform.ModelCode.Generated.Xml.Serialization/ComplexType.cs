
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.ModelCode.Generated.Xml.Serialization
{
    [XmlRoot(Namespace("IVySoft.VPlatform.ModelCode.Generated"))]
    public class ComplexType : IVySoft.VPlatform.ModelCode.Generated.ModuleType
    {
		[XmlElement()]
	        public bool Abstract { get; set; }

		
	public override object ToModel()
	{
		var result = new IVySoft.VPlatform.ModelCode.Generated.ComplexType();
		this.InitModel(result);
		return result;
	}

	protected void InitModel(IVySoft.VPlatform.ModelCode.Generated.ComplexType result)
	{
		base.InitModel(result);

		result.Abstract = this.Abstract;
					}
    }
}
