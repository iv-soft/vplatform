using IVySoft.VPlatform.TemplateEngine;
using IVySoft.VPlatform.TemplateEngine.Razor;
using IVySoft.VPlatform.TemplateService.Runtime.IndexScript;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateService.Razor
{
    public class RazorTemplateInstance : ParametersHolder
    {
        private readonly BuildContext context_;
        private readonly string file_path_;
        private readonly string build_file_path_;

        public RazorTemplateInstance(
            BuildContext context,
            string file_path)
        {
            this.context_ = context;
            this.file_path_ = file_path;

            this.build_file_path_ = PathUtils.RelativePath(context.SourceFolder, file_path);
            if (null == this.build_file_path_)
            {
                this.build_file_path_ = PathUtils.RelativePath(context.GlobalContext.ModulesFolder, file_path);
                if(null == this.build_file_path_)
                {
                    throw new System.Exception($"File {file_path} not in modules or source path");
                }
                else
                {
                    this.build_file_path_ = System.IO.Path.Combine(this.context_.BuildFolder, "razor_modules", this.build_file_path_ + ".dll");
                }
            }
            else
            {
                this.build_file_path_ = System.IO.Path.Combine(this.context_.BuildFolder, "razor", this.build_file_path_ + ".dll");
            }
        }

        public new RazorTemplateInstance with_parameter(string name, object value)
        {
            base.with_parameter(name, value);
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
            var script = templates.Load<RazorRuntimeBase>(
                System.IO.Path.Combine(this.context_.SourceFolder, this.file_path_),
                this.build_file_path_,
                options);
            if (!string.IsNullOrWhiteSpace(file_name)) { 
                script.Content = System.IO.File.ReadAllText(
                    System.IO.Path.Combine(this.context_.SourceFolder, file_name));
            }
            
            script.Context = this.context_;
            script.Parameters = this.Parameters;
            var body = script.Execute().Result;
            var dest_path = System.IO.Path.Combine(
                this.context_.GlobalContext.TargetFolder, output_path);
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(dest_path));
            System.IO.File.WriteAllText(dest_path, body);
        }
    }
}