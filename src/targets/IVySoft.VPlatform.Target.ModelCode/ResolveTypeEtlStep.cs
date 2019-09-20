using IVySoft.VPlatform.Etl.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IVySoft.VPlatform.Target.ModelCode
{
    public class ResolveTypeEtlStep : IEtlStep
    {
        public string[] Dependencies
        {
            get
            {
                return new string[]
                    {
                        typeof(ResolveNamespaceEtlStep).FullName
                    };
            }
        }

        public void Process(IEtlContext context)
        {
            var db = (IDataModel)context.DataModel;
            foreach(var module in db.Modules.Include(x => x.Types))
            {
                if(!module.IsExternal)
                {
                    foreach(var type in module.Types)
                    {
                        if(string.IsNullOrWhiteSpace(type.FullName))
                        {
                            type.FullName = module.Namespace + "." + type.Name;
                        }
                    }
                }
            }
            db.SaveChanges();

            foreach (var module in db.Modules.Include(x => x.Types).ThenInclude(y => y.Properties))
            {
                if (!module.IsExternal)
                {
                    foreach (var type in module.Types)
                    {
                        foreach(var property in type.Properties)
                        {
                            if (property.ResolvedType == null) {
                                var localType = module.Types.SingleOrDefault(x => x.Name == property.Type);
                                if (localType != null)
                                {
                                    property.ResolvedType = localType;
                                }
                                else
                                {
                                    foreach (var dependentModule in module.Dependencies)
                                    {
                                        var externalType = db.Modules.Single(x => x.Name == dependentModule.Name).Types.SingleOrDefault(x => x.Name == property.Type);
                                        if(externalType != null)
                                        {
                                            property.ResolvedType = externalType;
                                            break;
                                        }
                                    }

                                    if(property.ResolvedType == null)
                                    {
                                        throw new Exception($"Unable to resolve type {property.Type} of property {property.Name} in type {module.Name}.{type.Name}");
                                    }
                                }
                            }
                        }
                    }
                }
            }

            db.SaveChanges();
        }
    }
}
