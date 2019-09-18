using IVySoft.VPlatform.Generator.Core;
using IVySoft.VPlatform.TemplateEngine;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IVySoft.VPlatform.Target.ModelCode
{
    public class ModelCoreGenerator : IGenerator
    {
        public void Generate(IDbModel db, GeneratorOptions options)
        {
            var templates = new Templates(
                Path.Combine(Path.GetDirectoryName(typeof(ModelCoreGenerator).Assembly.Location)),
                    context => context.CompilerOptions.References.Add(
                        MetadataReference.CreateFromFile(typeof(ModelCoreGenerator).Assembly.Location)));
            var template = templates.Load<DataModelContext>("DataModels.cs.vtt");

            template.Model = (IDataModel)db;
            template.Options = options;

            template.Execute().Wait();
        }
    }
}
