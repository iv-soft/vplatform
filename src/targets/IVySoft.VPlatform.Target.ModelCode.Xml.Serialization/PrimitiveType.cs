using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.Target.ModelCode.Xml.Serialization
{
    [XmlRoot(Namespace = XmlConfig.Namespace)]
    public class PrimitiveType : IVySoft.VPlatform.Target.ModelCode.Xml.Serialization.ModuleType
    {



		public override object ToModel()
		{
			var result = new IVySoft.VPlatform.Target.ModelCode.PrimitiveType();
			this.InitModel(result);
			return result;
		}

		protected void InitModel(IVySoft.VPlatform.Target.ModelCode.PrimitiveType result)
		{
			base.InitModel(result);

				        }
    }
}