using IVySoft.VPlatform.TemplateEngine.EntityModel;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateEngine.IndexScript
{
    public class GlobalContext
    {
        public string SourceFolder { get; set; }
        public string TargetFolder { get; set; }
        public string BuildFolder { get; set; }
        public string ModulesFolder { get; set; }
        
        public Dictionary<string, EntityType> EntityTypes
        {
            get
            {
                return this.entityTypes;
            }
        }

        private Dictionary<string, EntityType> entityTypes = new Dictionary<string, EntityType>();

        private Dictionary<string, Module.ModuleCompile> modules = new Dictionary<string, Module.ModuleCompile>();
        private Dictionary<string, object> variables = new Dictionary<string, object>();

        internal Module.ModuleCompile ImportModule(string module_name)
        {
            Module.ModuleCompile result;
            if (!this.modules.TryGetValue(module_name, out result))
            {
                var context = new BuildContext
                {
                    SourceFolder = System.IO.Path.Combine(this.ModulesFolder, module_name),
                    BuildFolder = System.IO.Path.Combine(this.BuildFolder, module_name),
                    GlobalContext = this
                };

                result = new Module.ModuleCompile { Name = module_name, Context = context };
                this.modules.Add(module_name, result);

                context.Process();
            }
            return result;
        }

        internal List<T> GetCollection<T>(string name)
        {
            object value;
            if(!this.variables.TryGetValue(name, out value))
            {
                throw new Exception($"Collection {name} is not found");
            }

            return (List<T>)value;
        }

        internal void CreateCollection(string name, EntityType itemType)
        {
            var t = this.GetCLSType(itemType);
            this.SetVariable(name, Activator.CreateInstance(
                typeof(List<object>).GetGenericTypeDefinition().MakeGenericType(t)));
        }

        private void SetVariable(string name, object value)
        {
            this.variables[name] = value;
        }

        public Type GetCLSType(EntityType itemType)
        {
            if(null != itemType.CLSType)
            {
                return itemType.CLSType;
            }

            var input_file = System.IO.Path.Combine(
                this.ModulesFolder,
                "core",
                "entity",
                "entity.vcs");

            if (!System.IO.File.Exists(input_file))
            {
                throw new Exception($"File {input_file} not found");
            }

            var templates = new Templates(
                new TemplateCodeGeneratorOptions
                {
                    RootPath = System.IO.Path.Combine(this.ModulesFolder, "core", "entity"),
                    TempPath = this.BuildFolder,
                    BaseType = "IVySoft.VPlatform.TemplateEngine.TextTemplateWithModel<IVySoft.VPlatform.TemplateEngine.EntityModel.EntityType>"
                },
                context =>
                {
                    context.CompilerOptions.References.Add(
                        MetadataReference.CreateFromFile(typeof(EntityType).Assembly.Location));
                    context.CompilerOptions.References.Add(
                        MetadataReference.CreateFromFile(typeof(List<object>).GetGenericTypeDefinition().Assembly.Location));
                });

            var template = templates.Load<TextTemplateWithModel<EntityType>>(input_file);
            template.Model = itemType;
            var asm = templates.Compile(template.Execute().Result, 
                System.IO.Path.Combine(this.BuildFolder, "entity." + itemType.Namespace + "." + itemType.Name + ".dll"));

            itemType.CLSType = asm.GetType(itemType.Namespace + "." + itemType.Name);
            return itemType.CLSType;
        }

        internal EntityType AddEntityType(string name, string ns)
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
    }
}