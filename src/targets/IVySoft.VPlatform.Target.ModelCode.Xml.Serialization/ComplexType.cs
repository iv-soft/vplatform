using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.Target.ModelCode.Xml.Serialization
{
    [XmlRoot(Namespace = XmlConfig.Namespace)]
    public class ComplexType : IVySoft.VPlatform.Target.ModelCode.Xml.Serialization.ModuleType
    {
		[XmlElement()]
		public bool Abstract { get; set; }



		public override object ToModel()
		{
			var result = new IVySoft.VPlatform.Target.ModelCode.ComplexType();
			this.InitModel(result);
			return result;
		}

		protected void InitModel(IVySoft.VPlatform.Target.ModelCode.ComplexType result)
		{
			base.InitModel(result);

				result.Abstract = this.Abstract;
				        }
    }
}