using IVySoft.VPlatform.Generator.Core;
using IVySoft.VPlatform.Target.ModelCode;
using IVySoft.VPlatform.TemplateEngine;
using System.Collections.Generic;
using System.Linq;

namespace IVySoft.VPlatform.Target.ModelCode.Xml.Serialization
{
    public abstract class EntityTypeContext : TextTemplateBase
    {
        public IDataModel Model { get; internal set; }
        public GeneratorOptions Options { get; internal set; }
        public ModelCode.Module Module { get; internal set; }
        public ModelCode.EntityType EntityType { get; internal set; }

        public IEnumerable<ModelCode.ModuleType> DerivedTypes(ModelCode.ModuleType type)
        {
            var baseTypes = new List<string>();
            baseTypes.Add(type.FullName);
            while (baseTypes.Count > 0)
            {
                var baseType = baseTypes[0];
                baseTypes.RemoveAt(0);
                var result = Model.Modules.SelectMany(x => x.Types).Where(x => x.ResolvedBaseType != null && x.ResolvedBaseType.FullName == baseType).ToArray();
                foreach (var item in result)
                {
                    yield return item;
                    baseTypes.Add(item.FullName);
                }
            }
        }
    }
}