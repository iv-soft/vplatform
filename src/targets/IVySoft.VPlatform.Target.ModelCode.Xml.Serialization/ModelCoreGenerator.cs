using IVySoft.VPlatform.Generator.Core;
using IVySoft.VPlatform.Target.ModelCode;
using IVySoft.VPlatform.TemplateEngine;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IVySoft.VPlatform.Target.ModelCode.Xml.Serialization
{
    public class ModelCoreGenerator : IGenerator
    {
        public void Generate(IDbModel db, GeneratorOptions options)
        {
            var template = new Razor.DataModelsTemplate();

            template.Model = (IDataModel)db;
            template.Options = options;

            template.Execute().Wait();
        }
    }
}
