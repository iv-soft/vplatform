using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace IVySoft.VPlatform.Source.ModuleSchema
{
    [XmlRoot(Namespace = XmlConfig.Namespace)]
    public class Module
    {
        [XmlElement()]
        public string Name { get; set; }

        [XmlArray()]
        [XmlArrayItem(ElementName = "EntityType", Type = typeof(EntityType))]
        public EntityType[] Types { get; set; }

        public VPlatform.Target.ModelCode.Module ToModel()
        {
            return new Target.ModelCode.Module
            {
                Name = this.Name,
                Types = new List<Target.ModelCode.EntityType>(this.Types.Select(x => x.ToModel()))
            };
        }
    }
}
