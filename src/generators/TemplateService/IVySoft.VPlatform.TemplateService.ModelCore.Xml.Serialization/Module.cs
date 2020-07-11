
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization
{
    [XmlRoot(Namespace = "IVySoft.VPlatform.TemplateService.ModelCore")]
    public class Module
    {
		[XmlElement()]
	        public string Name { get; set; }
		[XmlElement()]
	        public string Namespace { get; set; }
		[XmlElement()]
	        public bool IsExternal { get; set; }

        [XmlArray()]
	public IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization.Association[] Associations { get; set; }
        [XmlArray()]
        [XmlArrayItem(ElementName = "TypeWithProperties", Type = typeof(IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization.TypeWithProperties))]
        [XmlArrayItem(ElementName = "PrimitiveType", Type = typeof(IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization.PrimitiveType))]
        [XmlArrayItem(ElementName = "EntityType", Type = typeof(IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization.EntityType))]
        [XmlArrayItem(ElementName = "ComplexType", Type = typeof(IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization.ComplexType))]
	public IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization.ModuleType[] Types { get; set; }
        [XmlArray()]
	public IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization.ModuleDependency[] Dependencies { get; set; }
        [XmlArray()]
	public IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization.EntityTable[] Tables { get; set; }
	
	public virtual object ToModel()
	{
		var result = new IVySoft.VPlatform.TemplateService.ModelCore.Module();
		this.InitModel(result);
		return result;
	}

	protected void InitModel(IVySoft.VPlatform.TemplateService.ModelCore.Module result)
	{

		result.Name = this.Name;
		result.Namespace = this.Namespace;
		result.IsExternal = this.IsExternal;
						result.Associations = new List<IVySoft.VPlatform.TemplateService.ModelCore.Association>((this.Associations == null) ? new IVySoft.VPlatform.TemplateService.ModelCore.Association[0] : this.Associations.Select(x => (IVySoft.VPlatform.TemplateService.ModelCore.Association)x.ToModel()));
		result.Types = new List<IVySoft.VPlatform.TemplateService.ModelCore.ModuleType>((this.Types == null) ? new IVySoft.VPlatform.TemplateService.ModelCore.ModuleType[0] : this.Types.Select(x => (IVySoft.VPlatform.TemplateService.ModelCore.ModuleType)x.ToModel()));
		result.Dependencies = new List<IVySoft.VPlatform.TemplateService.ModelCore.ModuleDependency>((this.Dependencies == null) ? new IVySoft.VPlatform.TemplateService.ModelCore.ModuleDependency[0] : this.Dependencies.Select(x => (IVySoft.VPlatform.TemplateService.ModelCore.ModuleDependency)x.ToModel()));
		result.Tables = new List<IVySoft.VPlatform.TemplateService.ModelCore.EntityTable>((this.Tables == null) ? new IVySoft.VPlatform.TemplateService.ModelCore.EntityTable[0] : this.Tables.Select(x => (IVySoft.VPlatform.TemplateService.ModelCore.EntityTable)x.ToModel()));
	}
    }
}
