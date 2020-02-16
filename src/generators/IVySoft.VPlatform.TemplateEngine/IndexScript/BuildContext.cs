using System;
using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateEngine.IndexScript
{
    public class BuildContext
    {
        public string SourceFolder { get; set; }
        public string BuildFolder { get; set; }
        public GlobalContext GlobalContext { get; set; }

        private Dictionary<string, Module.ModuleCompile> imports = new Dictionary<string, Module.ModuleCompile>();

        internal void ImportModule(string module_name)
        {
            Module.ModuleCompile module;
            if (!this.imports.TryGetValue(module_name, out module))
            {
                module = this.GlobalContext.ImportModule(module_name);
                this.imports.Add(module_name, module);
            }
        }

        internal void AddDirectory(string folder_name)
        {
            var context = new IndexScriptContext
            {
                CurrentFolder = folder_name,
                Context = this
            };

            new DirectoryBuildContext().Process(context);
        }
        public void Process()
        {
            this.AddDirectory(string.Empty);
        }
    }
}