using IVySoft.VPlatform.TemplateEngine;
using IVySoft.VPlatform.TemplateEngine.Razor;
using IVySoft.VPlatform.TemplateService.Runtime.IndexScript;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization.Module model;
            var serializer = new XmlSerializer(typeof(IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization.Module));
            using(var stream = new StreamReader(file_path, true))
            {
                model = (IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization.Module)serializer.Deserialize(stream);
            }

            var sp = this.get_db_model<ModelCore.DbModel>();
            using (var serviceScope = sp.CreateScope())
            {
                using (var db = serviceScope.ServiceProvider.GetService<ModelCore.DbModel>())
                {
                    db.Modules.Add((ModelCore.Module)model.ToModel());
                    db.SaveChanges();
                }
            }

            var target_path = System.IO.Path.Combine(this.context_.BuildFolder, "Models", model.Namespace);
            Directory.CreateDirectory(target_path);
            this.context_.GlobalContext.add_build_path(target_path, target_path);

            var razor = this.context_.GlobalContext.ServiceProvider.GetService<Razor.IRazorManager>();
            var cb = razor as IBuildContextDependent;
            if (cb != null)
            {
                cb.SetBuildContext(new BuildContext
                {
                    SourceFolder = System.IO.Path.Combine(this.context_.GlobalContext.ModulesFolder, "type_model"),
                    BuildFolder = target_path,
                    GlobalContext = this.context_.GlobalContext
                });
            }

            var entity_template = System.IO.Path.Combine(this.context_.GlobalContext.ModulesFolder, "type_model", "EntityType.cs");
            var complexType_template = System.IO.Path.Combine(this.context_.GlobalContext.ModulesFolder, "type_model", "ComplexType.cs");
            var db_model_template = System.IO.Path.Combine(this.context_.GlobalContext.ModulesFolder, "type_model", "DbModel.cs");
            var serialize_template = System.IO.Path.Combine(this.context_.GlobalContext.ModulesFolder, "xml_serialization", "Serializer.cs");

            var input_files = new List<string>();
            using (var serviceScope = sp.CreateScope())
            {
                using (var db = serviceScope.ServiceProvider.GetService<ModelCore.DbModel>())
                {
                    var module = db.Modules.Single(x => x.Namespace == model.Namespace);
                    Directory.CreateDirectory(System.IO.Path.Combine(target_path, module.Namespace));

                    foreach (ModelCore.EntityType entity_type in module.Types.Where(x => x is ModelCore.EntityType))
                    {
                        var target_file = System.IO.Path.Combine(target_path, module.Namespace, entity_type.Name + ".cs");

                        razor.load(entity_template)
                        .with("Namespace", module.Namespace)
                        .with("Name", entity_type.Name)
                        .process(target_file);
                        input_files.Add(target_file);
                    }
                    foreach (ModelCore.ComplexType type in module.Types.Where(x => x is ModelCore.ComplexType))
                    {
                        var target_file = System.IO.Path.Combine(target_path, module.Namespace, type.Name + ".cs");
                        razor.load(complexType_template)
                        .with("Namespace", module.Namespace)
                        .with("Name", type.Name)
                        .process(target_file);
                        input_files.Add(target_file);
                    }

                    var db_model_file = System.IO.Path.Combine(target_path, module.Namespace, "DbModel.cs");
                    razor.load(db_model_template)
                    .with("Namespace", module.Namespace)
                    .process(db_model_file);
                    input_files.Add(db_model_file);
                }
            }
            if (cb != null)
            {
                cb.ContextCompleted();
            }


            var dllPath = System.IO.Path.Combine(
                this.context_.GlobalContext.BuildFolder,
                "core",
                "entity",
                model.Namespace + ".dll");

            var asmPath = System.IO.Path.Combine(this.context_.GlobalContext.BuildFolder,
                "entity." + model.Namespace + ".dll");

            using (var serviceScope = sp.CreateScope())
            {
                using (var db = serviceScope.ServiceProvider.GetService<ModelCore.DbModel>())
                {
                    var asm = this.holder_.Compile(this.context_, input_files, dllPath, asmPath, db.Modules);
                    var model_type = asm.GetType(model.Namespace + ".DbModel");

                    var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
                    var configuration = builder.Build();

                    var services = new ServiceCollection();
                    services.AddSingleton<Microsoft.Extensions.Configuration.IConfiguration>(configuration);

                    services.AddEntityFrameworkInMemoryDatabase();
                    services.AddEntityFrameworkProxies();
                    var m = typeof(EntityFrameworkServiceCollectionExtensions)
                        .GetMethods().Single(
                        x =>
                        x.Name == "AddDbContext"
                        && x.IsStatic
                        && x.IsPublic
                        && x.GetGenericArguments().Length == 1
                        && x.GetParameters().Length > 1
                        && x.GetParameters()[1].ParameterType == typeof(Action<DbContextOptionsBuilder>))
                        .MakeGenericMethod(model_type);
                    var p = new object[m.GetParameters().Length];
                    p[0] = services;
                    p[1] = new Action<DbContextOptionsBuilder>(
                                    opt => {
                                        opt.UseInMemoryDatabase("InMemoryDb");
                                        opt.UseLazyLoadingProxies();
                                    });
                    for(int i = 2; i < m.GetParameters().Length; ++i)
                    {
                        p[i] = m.GetParameters()[i].DefaultValue;
                    }

                    m.Invoke(null, p);

                    var db_sp = services.BuildServiceProvider();

                    this.holder_.AddDbModel(model_type.FullName, db_sp);
                }
            }
        }
        public IServiceProvider get_db_model<T>()
        {
            return this.holder_.GetDbModel<T>();
        }

        public TModel load_model<TModel>(string file_path)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(TModel));
                using (var stream = new StreamReader(file_path, true))
                {
                    return (TModel)serializer.Deserialize(stream);
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message} at load model from '{file_path}'", ex);
            }
        }

    }
}