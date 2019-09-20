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

            foreach (var module in db.Modules)
            {
                if (!module.IsExternal)
                {
                    foreach (var type in module.Types)
                    {
                        if (type is EntityType)
                        {
                            var t = (EntityType)type;
                            foreach (var property in t.Properties)
                            {
                                if (property.ResolvedType == null)
                                {
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
                                            if (externalType != null)
                                            {
                                                property.ResolvedType = externalType;
                                                break;
                                            }
                                        }

                                        if (property.ResolvedType == null)
                                        {
                                            throw new Exception($"Unable to resolve type {property.Type} of property {property.Name} in type {module.Name}.{type.Name}");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            db.SaveChanges();

            foreach (var module in db.Modules)
            {
                foreach (var type in module.Types)
                {
                    if (type.ResolvedBaseType == null && !string.IsNullOrWhiteSpace(type.BaseType))
                    {
                        var localType = module.Types.SingleOrDefault(x => x.Name == type.BaseType);
                        if (localType != null)
                        {
                            type.ResolvedBaseType = localType;
                        }
                        else
                        {
                            foreach (var dependentModule in module.Dependencies)
                            {
                                var externalType = db.Modules.Single(x => x.Name == dependentModule.Name).Types.SingleOrDefault(x => x.Name == type.BaseType);
                                if (externalType != null)
                                {
                                    type.ResolvedBaseType = externalType;
                                    break;
                                }
                            }

                            if (type.ResolvedBaseType== null)
                            {
                                throw new Exception($"Unable to resolve base type {type.BaseType} of type {module.Name}.{type.Name}");
                            }
                        }
                    }
                }
            }

            db.SaveChanges();
            foreach (var module in db.Modules)
            {
                foreach (var association in module.Associations)
                {
                    if (association.Left.ResolvedType == null)
                    {
                        var localType = module.Types.SingleOrDefault(x => x.Name == association.Left.Type);
                        if (localType != null)
                        {
                            association.Left.ResolvedType = localType;
                        }
                        else
                        {
                            foreach (var dependentModule in module.Dependencies)
                            {
                                var externalType = db.Modules.Single(x => x.Name == dependentModule.Name).Types.SingleOrDefault(x => x.Name == association.Left.Type);
                                if (externalType != null)
                                {
                                    association.Left.ResolvedType = externalType;
                                    break;
                                }
                            }

                            if (association.Left.ResolvedType == null)
                            {
                                throw new Exception($"Unable to resolve left end type {association.Left.Type} of association {module.Name}.{association.Name}");
                            }
                        }
                    }
                    if (association.Right.ResolvedType == null)
                    {
                        var localType = module.Types.SingleOrDefault(x => x.Name == association.Right.Type);
                        if (localType != null)
                        {
                            association.Right.ResolvedType = localType;
                        }
                        else
                        {
                            foreach (var dependentModule in module.Dependencies)
                            {
                                var externalType = db.Modules.Single(x => x.Name == dependentModule.Name).Types.SingleOrDefault(x => x.Name == association.Right.Type);
                                if (externalType != null)
                                {
                                    association.Right.ResolvedType = externalType;
                                    break;
                                }
                            }

                            if (association.Right.ResolvedType == null)
                            {
                                throw new Exception($"Unable to resolve right end type {association.Right.Type} of association {module.Name}.{association.Name}");
                            }
                        }
                    }
                }
            }

            db.SaveChanges();
        }
    }
}
