using System;
using System.Xml.Serialization;
using IVySoft.VPlatform.Target.ModelCode;

namespace IVySoft.VPlatform.Source.ModuleSchema
{
    public class Property
    {
        [XmlElement()]
        public string Name { get; set; }

        [XmlElement()]
        public string Type { get; set; }

        [XmlElement()]
        public string Multiplicity { get; set; }

        [XmlElement()]
        public string Default { get; set; }

        internal Target.ModelCode.Property ToModel()
        {
            return new Target.ModelCode.Property
            {
                Name = this.Name,
                Type = this.Type,
                Multiplicity = this.Multiplicity,
                Default = this.Default
            };
        }
    }
}