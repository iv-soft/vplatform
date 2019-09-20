using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.Target.ModelCode.Xml.Serialization
{
    [XmlRoot(Namespace = XmlConfig.Namespace)]
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
		[XmlElement()]
		public string FullName { get; set; }



		public virtual object ToModel()
		{
			var result = new IVySoft.VPlatform.Target.ModelCode.ModuleType();
			this.InitModel(result);
			return result;
		}

		protected void InitModel(IVySoft.VPlatform.Target.ModelCode.ModuleType result)
		{

				result.Name = this.Name;
				result.BaseType = this.BaseType;
				result.Discriminator = this.Discriminator;
				result.ElementName = this.ElementName;
				result.FullName = this.FullName;
				        }
    }
}