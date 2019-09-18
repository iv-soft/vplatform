using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;
using IVySoft.VPlatform.Target.ModelCode;

namespace IVySoft.VPlatform.Source.ModuleSchema
{
    public class EntityType
    {
        [XmlElement()]
        public string Name { get; set; }

        [XmlArray()]
        [XmlArrayItem(ElementName = "Property", Type = typeof(Property))]
        public Property[] Properties { get; set; }

        internal Target.ModelCode.EntityType ToModel()
        {
            return new Target.ModelCode.EntityType
            {
                Name = this.Name,
                Properties = new List<VPlatform.Target.ModelCode.Property>(
                    this.Properties.Select(x => x.ToModel()))
            };
        }
    }
}