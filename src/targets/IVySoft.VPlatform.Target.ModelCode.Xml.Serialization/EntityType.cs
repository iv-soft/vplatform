using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.Target.ModelCode.Xml.Serialization
{
    [XmlRoot(Namespace = XmlConfig.Namespace)]
    public class EntityType : IVySoft.VPlatform.Target.ModelCode.Xml.Serialization.ModuleType
    {
		[XmlElement()]
		public bool Abstract { get; set; }

        [XmlArray()]
		public Property[] Properties { get; set; }


		public override object ToModel()
		{
			var result = new IVySoft.VPlatform.Target.ModelCode.EntityType();
			this.InitModel(result);
			return result;
		}

		protected void InitModel(IVySoft.VPlatform.Target.ModelCode.EntityType result)
		{
			base.InitModel(result);

				result.Abstract = this.Abstract;
				result.Properties = new List<IVySoft.VPlatform.Target.ModelCode.Property>((this.Properties == null) ? new IVySoft.VPlatform.Target.ModelCode.Property[0] : this.Properties.Select(x => (IVySoft.VPlatform.Target.ModelCode.Property)x.ToModel()));
		        }
    }
}