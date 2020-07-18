using IVySoft.VPlatform.TemplateEngine;
using IVySoft.VPlatform.TemplateEngine.Razor;
using IVySoft.VPlatform.TemplateService.Runtime.IndexScript;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using IVySoft.VPlatform.TemplateService.ModelCore;

namespace IVySoft.VPlatform.TemplateService.Entity
{
    internal class EntityModelHolder : IEntityModelHolder
    {
        private Dictionary<string, EntityType> entityTypes = new Dictionary<string, EntityType>();
        private readonly ITemplateCompiler compiler_;
        private Dictionary<string, IServiceProvider> db_modules_ = new Dictionary<string, IServiceProvider>();

        public EntityModelHolder(ITemplateCompiler compiler)
        {
            this.compiler_ = compiler;

            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
            var configuration = builder.Build();

            var services = new ServiceCollection();
            services.AddSingleton<Microsoft.Extensions.Configuration.IConfiguration>(configuration);

            services.AddEntityFrameworkInMemoryDatabase();
            services.AddEntityFrameworkProxies();
            services.AddDbContext<DbModel>(opt =>
            {
                opt.UseInMemoryDatabase("InMemoryDb");
                opt.UseLazyLoadingProxies();
            });

            this.AddDbModel(typeof(DbModel).FullName, services.BuildServiceProvider());
        }

        public Dictionary<string, EntityType> EntityTypes
        {
            get
            {
                return this.entityTypes;
            }
        }

        public Type GetCLSType(BuildContext context, EntityType itemType)
        {
            if (null != itemType.CLSType)
            {
                return itemType.CLSType;
            }

            var input_file = System.IO.Path.Combine(
                context.GlobalContext.ModulesFolder,
                "core",
                "entity",
                "entity.vcs");

            var dllPath = System.IO.Path.Combine(
                context.GlobalContext.BuildFolder,
                "core",
                "entity",
                "entity.vcs.dll");

            var asmPath = System.IO.Path.Combine(context.GlobalContext.BuildFolder,
                "entity." + itemType.Namespace + "." + itemType.Name + ".dll");

            var asm = this.Compile(context, input_file, dllPath, asmPath, itemType);
            context.GlobalContext.References.Add(MetadataReference.CreateFromFile(asm.Location));
            itemType.CLSType = asm.GetType(itemType.Namespace + "." + itemType.Name);
            return itemType.CLSType;
        }

        public Assembly Compile<T>(BuildContext context, string input_file, string dllPath, string asmPath, T model)
        {
            if (!System.IO.File.Exists(input_file))
            {
                throw new Exception($"File {input_file} not found");
            }

            var templates = new RazorTemplates(
                this.compiler_,
                new TemplateCodeGeneratorOptions
                {
                    RootPath = System.IO.Path.Combine(context.GlobalContext.ModulesFolder, "core", "entity"),
                    TempPath = context.GlobalContext.BuildFolder,
                    BaseType = typeof(TextTemplateWithModel<T>).UserFriendlyName()
                });
            var options = new CompilerOptions
            {
                References = new List<MetadataReference>(
                        new MetadataReference[]
                        {
                            MetadataReference.CreateFromFile(
                                typeof(T).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(List<object>).GetGenericTypeDefinition().Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(System.Xml.Serialization.XmlRootAttribute).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(IVySoft.VPlatform.TemplateService.ModelCore.ModuleType).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(RelationalEntityTypeBuilderExtensions).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(Microsoft.Extensions.Configuration.ConfigurationBuilder).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(ServiceCollection).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(Microsoft.Extensions.Configuration.IConfigurationRoot).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(InMemoryServiceCollectionExtensions).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(ServiceCollectionServiceExtensions).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(ProxiesServiceCollectionExtensions).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(IServiceProvider).Assembly.Location),
                        }
                    )
            };

            var template = templates.Load<TextTemplateWithModel<T>>(
                input_file,
                dllPath,
                options);
            template.Model = model;
            var asm = templates.Compile(template.Execute().Result, asmPath, options);
            context.GlobalContext.References.Add(MetadataReference.CreateFromFile(asm.Location));
            return asm;
        }
        public Assembly Compile<T>(BuildContext context, IEnumerable<string> input_files, string dllPath, string asmPath, T model)
        {
            var templates = new RazorTemplates(
                this.compiler_,
                new TemplateCodeGeneratorOptions
                {
                    RootPath = System.IO.Path.Combine(context.GlobalContext.ModulesFolder, "core", "entity"),
                    TempPath = context.GlobalContext.BuildFolder,
                    BaseType = typeof(TextTemplateWithModel<T>).UserFriendlyName()
                });
            var options = new CompilerOptions
            {
                References = new List<MetadataReference>(
                        new MetadataReference[]
                        {
                            MetadataReference.CreateFromFile(
                                typeof(T).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(List<object>).GetGenericTypeDefinition().Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(System.Xml.Serialization.XmlRootAttribute).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(IVySoft.VPlatform.TemplateService.ModelCore.ModuleType).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(OwnedAttribute).Assembly.Location),                            
                            MetadataReference.CreateFromFile(
                                typeof(RelationalEntityTypeBuilderExtensions).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(Microsoft.Extensions.Configuration.ConfigurationBuilder).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(ServiceCollection).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(Microsoft.Extensions.Configuration.IConfigurationRoot).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(InMemoryServiceCollectionExtensions).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(ServiceCollectionServiceExtensions).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(ProxiesServiceCollectionExtensions).Assembly.Location),
                            MetadataReference.CreateFromFile(
                                typeof(IServiceProvider).Assembly.Location),
                        }
                    )
            };

            var asm = templates.Compile(input_files, asmPath, options);
            context.GlobalContext.References.Add(MetadataReference.CreateFromFile(asm.Location));
            return asm;
        }

        public EntityType AddEntityType(BuildContext context, string name, string ns)
        {
            if (this.entityTypes.ContainsKey(ns + "." + name))
            {
                throw new Exception($"Entity type {ns}.{name} already exists");
            }

            var result = new EntityType
            {
                Name = name,
                Namespace = ns
            };

            this.entityTypes.Add(ns + "." + name, result);

            return result;
        }

        public void AddDbModel(string fullName, IServiceProvider serviceProvider)
        {
            this.db_modules_.Add(fullName, serviceProvider);
        }

        public IServiceProvider GetDbModel<T>()
        {
            return this.db_modules_[typeof(T).FullName];
        }
    }
}