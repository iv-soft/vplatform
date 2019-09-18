using IVySoft.VPlatform.Generator.Core;
using IVySoft.VPlatform.TemplateEngine;

namespace IVySoft.VPlatform.Target.ModelCode
{
    public abstract class EntityTypeContext : TextTemplateBase
    {
        public IDataModel Model { get; internal set; }
        public GeneratorOptions Options { get; internal set; }
        public EntityType EntityType { get; internal set; }
    }
}