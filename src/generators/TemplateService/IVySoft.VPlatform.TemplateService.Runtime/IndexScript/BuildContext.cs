using IVySoft.VPlatform.TemplateEngine;
using IVySoft.VPlatform.TemplateEngine.Script;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.TemplateService.Runtime.IndexScript
{
    public class BuildContext
    {
        public string SourceFolder { get; set; }
        public string BuildFolder { get; set; }
        public GlobalContext GlobalContext { get; set; }

        private Dictionary<string, ModuleCompile> imports = new Dictionary<string, ModuleCompile>();

        internal void ImportModule(string module_name)
        {
            ModuleCompile module;
            if (!this.imports.TryGetValue(module_name, out module))
            {
                module = this.GlobalContext.ImportModule(module_name);
                this.imports.Add(module_name, module);
            }
        }

        public bool TryGetVariable(string name, out object value)
        {
            return this.GlobalContext.TryGetVariable(name, out value);
        }

        public void SetVariable(string name, object value)
        {
            this.GlobalContext.SetVariable(name, value);
        }

        internal void AddDirectory(string folder_name)
        {
            var context = new IndexScriptContext
            {
                CurrentFolder = folder_name,
                Context = this
            };

            var index_file = System.IO.Path.Combine(
                context.Context.SourceFolder,
                context.CurrentFolder,
                "index.vgen");
            if (!System.IO.File.Exists(index_file))
            {
                throw new Exception($"File {index_file} not found");
            }

            var templates = new ScriptTemplates(new TemplateCodeGeneratorOptions
            {
                RootPath = System.IO.Path.Combine(context.Context.SourceFolder, context.CurrentFolder),
                TempPath = System.IO.Path.Combine(context.Context.BuildFolder, context.CurrentFolder),
                TemplateTypeName = "IndexScript",
                BaseType = typeof(IndexScript.IndexScriptBase).FullName
            },
            options =>
            {
                options.CompilerOptions.References.AddRange(this.GlobalContext.References);
            });

            System.IO.Directory.CreateDirectory(templates.GeneratorOptions.RootPath);
            System.IO.Directory.CreateDirectory(templates.GeneratorOptions.TempPath);

            var script = templates.Load<IndexScript.IndexScriptBase>(index_file);
            script.Context = context;
            script.Execute();
        }
        public void Process()
        {
            this.AddDirectory(string.Empty);
        }
    }
}
