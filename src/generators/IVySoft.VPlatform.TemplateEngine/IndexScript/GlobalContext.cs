using IVySoft.VPlatform.TemplateEngine.EntityModel;
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

        private Dictionary<string, EntityType> entityTypes = new Dictionary<string, EntityType>();

        private Dictionary<string, Module.ModuleCompile> modules = new Dictionary<string, Module.ModuleCompile>();

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