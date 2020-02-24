using IVySoft.VPlatform.TemplateEngine;
using IVySoft.VPlatform.TemplateEngine.Razor;
using IVySoft.VPlatform.TemplateService.Runtime.IndexScript;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateService.Razor
{
    public class RazorTemplateInstance
    {
        private readonly Dictionary<string, object> parameters_ = new Dictionary<string, object>();
        private readonly BuildContext context_;
        private readonly string file_path_;

        public RazorTemplateInstance(
            BuildContext context,
            string file_path)
        {
            this.context_ = context;
            this.file_path_ = file_path;
        }

        public RazorTemplateInstance with_parameter(string name, object value)
        {
            this.parameters_.Add(name, value);
            return this;
        }

        public void process(string output_path)
        {
            this.process(string.Empty, output_path);
        }

        public void process(string file_name, string output_path)
        {
            var templates = new RazorTemplates(
                this.context_.GlobalContext.ServiceProvider.GetRequiredService<ITemplateCompiler>(),
                new TemplateCodeGeneratorOptions
            {
                RootPath = this.context_.SourceFolder,
                TempPath = this.context_.BuildFolder,
                TemplateTypeName = "TemplateRuntime",
                BaseType = typeof(RazorRuntimeBase).FullName
            });

            var options = new CompilerOptions
            {
                References = new List<MetadataReference>(this.context_.GlobalContext.References)
            };
            var dllPath = System.IO.Path.Combine(this.context_.BuildFolder, this.file_path_ + ".dll");
            var script = templates.Load<RazorRuntimeBase>(
                System.IO.Path.Combine(this.context_.SourceFolder, this.file_path_),
                dllPath,
                options);
            if (!string.IsNullOrWhiteSpace(file_name)) { 
                script.Content = System.IO.File.ReadAllText(
                    System.IO.Path.Combine(this.context_.SourceFolder, file_name));
            }
            
            script.Context = this.context_;
            script.Parameters = this.parameters_;
            var body = script.Execute().Result;
            var dest_path = System.IO.Path.Combine(
                this.context_.GlobalContext.TargetFolder, output_path);
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(dest_path));
            System.IO.File.WriteAllText(dest_path, body);
        }
    }
}