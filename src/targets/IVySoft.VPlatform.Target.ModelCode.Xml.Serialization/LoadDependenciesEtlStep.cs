using System;
using System.Collections.Generic;
using System.Linq;
using IVySoft.VPlatform.Etl.Core;
using IVySoft.VPlatform.Generator.Core;
using IVySoft.VPlatform.Source.Xml;
using Microsoft.EntityFrameworkCore;

namespace IVySoft.VPlatform.Target.ModelCode.Xml.Serialization
{
    public class LoadDependenciesEtlStep : IEtlStep
    {
        public string[] Dependencies
        {
            get
            {
                return null;
            }
        }


        public void Process(IEtlContext context)
        {
            var external = new Dictionary<string, ModelCode.Module>();
            foreach(var file in System.IO.Directory.GetFiles(context.Get<GeneratorOptions>().ModulesFolder, "*.xml"))
            {
                var source = new XmlSource<Module> { FilePath = file };
                var module = (ModelCode.Module)source.Load().ToModel();
                module.IsExternal = true;
                external.Add(module.Name, module);
            }

            var db = (IDataModel)context.DataModel;
            for (; ; )
            {
                bool added = false;
                foreach (var module in db.Modules.Include(x => x.Dependencies).ToArray())
                {
                    if (!module.IsExternal)
                    {
                        foreach (var dependency in module.Dependencies)
                        {
                            if (!db.Modules.Any(x => x.Name == dependency.Name))
                            {
                                ModelCode.Module dependModule;
                                if (!external.TryGetValue(dependency.Name, out dependModule))
                                {
                                    throw new Exception($"Module {dependency.Name} not found");
                                }

                                db.Modules.Add(dependModule);
                                db.SaveChanges();
                                added = true;
                            }
                        }
                    }
                }
                if (!added)
                {
                    break;
                }
            }
        }
    }
}