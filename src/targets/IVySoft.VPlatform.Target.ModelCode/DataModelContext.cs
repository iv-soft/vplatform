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

        public void Generate(string templateFile, EntityType entityType, string targetFile)
        {
            var templates = new Templates(
                Path.Combine(Path.GetDirectoryName(typeof(DataModelContext).Assembly.Location)),
                context => context.CompilerOptions.References.Add(
                    MetadataReference.CreateFromFile(typeof(DataModelContext).Assembly.Location)));
            var template = templates.Load<EntityTypeContext>(templateFile);

            template.Model = this.Model;
            template.Options = this.Options;
            template.EntityType = entityType;

            File.WriteAllText(targetFile, template.Execute().Result);
        }
    }
}