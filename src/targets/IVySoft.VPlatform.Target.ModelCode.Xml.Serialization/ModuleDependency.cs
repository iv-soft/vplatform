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



		public IVySoft.VPlatform.Target.ModelCode.ModuleDependency ToModel()
        {
            return new IVySoft.VPlatform.Target.ModelCode.ModuleDependency
            {
				Name = this.Name,
				            };
        }

    }
}