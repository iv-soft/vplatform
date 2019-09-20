using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.Target.ModelCode.Xml.Serialization
{
    [XmlRoot(Namespace = XmlConfig.Namespace)]
    public class Module
    {
		[XmlElement()]
		public string Name { get; set; }
		[XmlElement()]
		public string Namespace { get; set; }
		[XmlElement()]
		public bool IsExternal { get; set; }

        [XmlArray()]
		public Association[] Associations { get; set; }
        [XmlArray()]
		public EntityType[] Types { get; set; }
        [XmlArray()]
		public ModuleDependency[] Dependencies { get; set; }


		public IVySoft.VPlatform.Target.ModelCode.Module ToModel()
        {
            return new IVySoft.VPlatform.Target.ModelCode.Module
            {
					Name = this.Name,
					Namespace = this.Namespace,
					IsExternal = this.IsExternal,
                Associations = new List<ModelCode.Association>((this.Associations == null) ? new ModelCode.Association[0] : this.Associations.Select(x => x.ToModel())),
                Types = new List<ModelCode.EntityType>((this.Types == null) ? new ModelCode.EntityType[0] : this.Types.Select(x => x.ToModel())),
                Dependencies = new List<ModelCode.ModuleDependency>((this.Dependencies == null) ? new ModelCode.ModuleDependency[0] : this.Dependencies.Select(x => x.ToModel())),

            };
        }

    }
}