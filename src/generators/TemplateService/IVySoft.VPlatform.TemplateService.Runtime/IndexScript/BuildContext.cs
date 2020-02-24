using IVySoft.VPlatform.TemplateEngine;
using IVySoft.VPlatform.TemplateEngine.Script;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
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

        internal void ImportModule(string module_name)
        {
            this.GlobalContext.ImportModule(module_name);
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
            var ctx = new BuildContext
            {
                SourceFolder = System.IO.Path.Combine(this.SourceFolder, folder_name),
                BuildFolder = System.IO.Path.Combine(this.BuildFolder, folder_name),
                GlobalContext = this.GlobalContext
            };
            
            var context = new IndexScriptContext
            {
                Context = ctx
            };

            var index_file = System.IO.Path.Combine(
                ctx.SourceFolder,
                "index.vgen");
            if (!System.IO.File.Exists(index_file))
            {
                throw new Exception($"File {index_file} not found");
            }

            var templates = new ScriptTemplates(
                this.GlobalContext.ServiceProvider.GetRequiredService<ITemplateCompiler>(),
                new TemplateCodeGeneratorOptions
            {
                RootPath = ctx.SourceFolder,
                TempPath = ctx.BuildFolder,
                TemplateTypeName = "IndexScript",
                BaseType = typeof(IndexScript.IndexScriptBase).FullName
            });

            System.IO.Directory.CreateDirectory(templates.GeneratorOptions.RootPath);
            System.IO.Directory.CreateDirectory(templates.GeneratorOptions.TempPath);

            var options = new CompilerOptions
            {
                References = new List<MetadataReference>(this.GlobalContext.References)
            };
            var dllPath = System.IO.Path.Combine(
                ctx.BuildFolder,
                "index.vgen.dll");
            var script = templates.Load<IndexScript.IndexScriptBase>(index_file, dllPath, options);
            script.Context = context;
            script.Execute();
        }
        public void Process()
        {
            this.AddDirectory(string.Empty);
        }
    }
}
