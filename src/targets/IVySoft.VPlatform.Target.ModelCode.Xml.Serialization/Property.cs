using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.Target.ModelCode.Xml.Serialization
{
    [XmlRoot(Namespace = XmlConfig.Namespace)]
    public class Property
    {
		[XmlElement()]
		public string Name { get; set; }
		[XmlElement()]
		public string Type { get; set; }
		[XmlElement()]
		public bool Nullable { get; set; }
		[XmlElement()]
		public bool XmlIgnore { get; set; }
		[XmlElement()]
		public string Default { get; set; }



		public virtual object ToModel()
		{
			var result = new IVySoft.VPlatform.Target.ModelCode.Property();
			this.InitModel(result);
			return result;
		}

		protected void InitModel(IVySoft.VPlatform.Target.ModelCode.Property result)
		{

				result.Name = this.Name;
				result.Type = this.Type;
				result.Nullable = this.Nullable;
				result.XmlIgnore = this.XmlIgnore;
				result.Default = this.Default;
				        }
    }
}