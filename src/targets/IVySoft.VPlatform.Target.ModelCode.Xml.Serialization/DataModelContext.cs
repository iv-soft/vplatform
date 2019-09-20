using IVySoft.VPlatform.Generator.Core;
using IVySoft.VPlatform.Target.ModelCode;
using IVySoft.VPlatform.TemplateEngine;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.IO;

namespace IVySoft.VPlatform.Target.ModelCode.Xml.Serialization
{
    public abstract class DataModelContext : TextTemplateBase
    {
        public IDataModel Model { get; set; }

        public GeneratorOptions Options { get; set; }

        public void Generate_EntityType(ModelCode.Module module, ModelCode.EntityType entityType, string targetFile)
        {
            var template = new Razor.EntityTypeTemplate
            {
                Model = this.Model,
                Module = module,
                Options = this.Options,
                EntityType = entityType
            };

            File.WriteAllText(targetFile, template.Execute().Result);
        }
    }
}