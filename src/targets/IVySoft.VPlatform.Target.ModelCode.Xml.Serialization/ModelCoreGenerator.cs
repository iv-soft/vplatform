using IVySoft.VPlatform.Etl.Core;
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
    public class ModelCoreGenerator : IEtlStep
    {
        public string[] Dependencies
        {
            get
            {
                return new string[]
                    {
                        typeof(ResolveTypeEtlStep).FullName
                    };
            }
        }

        public void Process(IEtlContext context)
        {
            var template = new Razor.DataModelsTemplate();

            template.Model = (IDataModel)context.DataModel;
            template.Options = context.Get<GeneratorOptions>();

            template.Execute().Wait();
        }
    }
}
