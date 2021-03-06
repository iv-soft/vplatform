using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.Target.ModelCode.Xml.Serialization
{
    [XmlRoot(Namespace = XmlConfig.Namespace)]
    public class ModuleDependency
    {
		[XmlElement()]
		public string Name { get; set; }



		public virtual object ToModel()
		{
			var result = new IVySoft.VPlatform.Target.ModelCode.ModuleDependency();
			this.InitModel(result);
			return result;
		}

		protected void InitModel(IVySoft.VPlatform.Target.ModelCode.ModuleDependency result)
		{

				result.Name = this.Name;
				        }
    }
}