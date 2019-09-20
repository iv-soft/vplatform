using IVySoft.VPlatform.Generator.Core;
using IVySoft.VPlatform.Target.ModelCode;
using IVySoft.VPlatform.TemplateEngine;

namespace IVySoft.VPlatform.Target.ModelCode.Xml.Serialization
{
    public abstract class EntityTypeContext : TextTemplateBase
    {
        public IDataModel Model { get; internal set; }
        public GeneratorOptions Options { get; internal set; }
        public ModelCode.Module Module { get; internal set; }
        public ModelCode.EntityType EntityType { get; internal set; }
    }
}