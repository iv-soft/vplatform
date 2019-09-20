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
        [XmlArrayItem(ElementName = "EntityType", Type = typeof(IVySoft.VPlatform.Target.ModelCode.Xml.Serialization.EntityType))]
        [XmlArrayItem(ElementName = "ComplexType", Type = typeof(IVySoft.VPlatform.Target.ModelCode.Xml.Serialization.ComplexType))]
        [XmlArrayItem(ElementName = "PrimitiveType", Type = typeof(IVySoft.VPlatform.Target.ModelCode.Xml.Serialization.PrimitiveType))]
		public ModuleType[] Types { get; set; }
        [XmlArray()]
		public ModuleDependency[] Dependencies { get; set; }


		public virtual object ToModel()
		{
			var result = new IVySoft.VPlatform.Target.ModelCode.Module();
			this.InitModel(result);
			return result;
		}

		protected void InitModel(IVySoft.VPlatform.Target.ModelCode.Module result)
		{

				result.Name = this.Name;
				result.Namespace = this.Namespace;
				result.IsExternal = this.IsExternal;
				result.Associations = new List<IVySoft.VPlatform.Target.ModelCode.Association>((this.Associations == null) ? new IVySoft.VPlatform.Target.ModelCode.Association[0] : this.Associations.Select(x => (IVySoft.VPlatform.Target.ModelCode.Association)x.ToModel()));
		result.Types = new List<IVySoft.VPlatform.Target.ModelCode.ModuleType>((this.Types == null) ? new IVySoft.VPlatform.Target.ModelCode.ModuleType[0] : this.Types.Select(x => (IVySoft.VPlatform.Target.ModelCode.ModuleType)x.ToModel()));
		result.Dependencies = new List<IVySoft.VPlatform.Target.ModelCode.ModuleDependency>((this.Dependencies == null) ? new IVySoft.VPlatform.Target.ModelCode.ModuleDependency[0] : this.Dependencies.Select(x => (IVySoft.VPlatform.Target.ModelCode.ModuleDependency)x.ToModel()));
		        }
    }
}