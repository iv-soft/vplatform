using IVySoft.VPlatform.TemplateEngine;
using IVySoft.VPlatform.TemplateEngine.Razor;
using IVySoft.VPlatform.TemplateService.Runtime.IndexScript;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace IVySoft.VPlatform.TemplateService.Entity
{
    internal class EntityManager : IEntityManager, IBuildContextDependent
    {
        private readonly IEntityModelHolder holder_;
        private BuildContext context_;
        private readonly List<EntityType> entityTypes_ = new List<EntityType>();

        public EntityManager(IEntityModelHolder holder)
        {
            this.holder_ = holder;
        }

        public List<T> get_collection<T>(string name)
        {
            object value;
            if (!this.context_.TryGetVariable(name, out value))
            {
                throw new Exception($"Collection {name} is not found");
            }

            return (List<T>)value;
        }

        public void create_collection(string name, EntityType itemType)
        {
            var t = this.GetCLSType(itemType);
            this.context_.SetVariable(name, Activator.CreateInstance(
                typeof(List<object>).GetGenericTypeDefinition().MakeGenericType(t)));
        }

        public Type GetCLSType(EntityType itemType)
        {
            return this.holder_.GetCLSType(this.context_, itemType);
        }

        public EntityType add_entity_type(string name, string ns)
        {
            var result = this.holder_.AddEntityType(this.context_, name, ns);
            this.entityTypes_.Add(result);
            return result;
        }

        public void SetBuildContext(BuildContext context)
        {
            this.context_ = context;
        }

        public void ContextCompleted()
        {
            foreach(var t in this.entityTypes_)
            {
                this.GetCLSType(t);
            }
        }

        public void import_module(string file_path)
        {
            IVySoft.VPlatform.ModelCode.Xml.Serialization.Module module;
            var serializer = new XmlSerializer(typeof(IVySoft.VPlatform.ModelCode.Xml.Serialization.Module));
            using(var stream = new StreamReader(file_path, true))
            {
                module = (IVySoft.VPlatform.ModelCode.Xml.Serialization.Module)serializer.Deserialize(stream);
            }

            var sp = ModelCode.DbModel.CreateServiceProvider();
            using (var serviceScope = sp.CreateScope())
            {
                using (var db = serviceScope.ServiceProvider.GetService<ModelCode.DbModel>())
                {
                    db.Modules.Add((ModelCode.Module)module.ToModel());
                    db.SaveChanges();
                }
            }

            var input_file = System.IO.Path.Combine(
                this.context_.GlobalContext.ModulesFolder,
                "core",
                "entity",
                "modules.cs");

            var dllPath = System.IO.Path.Combine(
                this.context_.GlobalContext.BuildFolder,
                "core",
                "entity",
                module.Namespace + ".dll");

            var asmPath = System.IO.Path.Combine(this.context_.GlobalContext.BuildFolder,
                "entity." + module.Namespace + ".dll");

            using (var serviceScope = sp.CreateScope())
            {
                using (var db = serviceScope.ServiceProvider.GetService<ModelCode.DbModel>())
                {
                    var asm = this.holder_.Compile(this.context_, input_file, dllPath, asmPath, db.Modules);
                    var model_type = asm.GetType(module.Namespace + ".DbModel");
                    this.holder_.AddDbModel(model_type.FullName, (IServiceProvider)model_type.GetMethod("CreateServiceProvider").Invoke(null, null));
                }
            }
        }
        public IServiceProvider get_db_model<T>()
        {
            return this.holder_.GetDbModel<T>();
        }

    }
}