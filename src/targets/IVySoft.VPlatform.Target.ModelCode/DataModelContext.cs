using IVySoft.VPlatform.Generator.Core;
using IVySoft.VPlatform.TemplateEngine;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.IO;

namespace IVySoft.VPlatform.Target.ModelCode
{
    public abstract class DataModelContext : TextTemplateBase
    {
        public IDataModel Model { get; internal set; }

        public GeneratorOptions Options { get; internal set; }

        public void Generate_EntityType(EntityType entityType, string targetFile)
        {
            var template = new Razor.EntityTypeTemplate
            {
                Model = this.Model,
                Options = this.Options,
                EntityType = entityType
            };

            File.WriteAllText(targetFile, template.Execute().Result);
        }
    }
}