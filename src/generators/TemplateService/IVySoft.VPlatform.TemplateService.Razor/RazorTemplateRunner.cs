using IVySoft.VPlatform.TemplateEngine;
using IVySoft.VPlatform.TemplateEngine.Razor;
using IVySoft.VPlatform.TemplateService.Runtime.IndexScript;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateService.Razor
{
    public class RazorTemplateRunner : ParametersHolder
    {
        protected readonly BuildContext context_;
        protected readonly string file_path_;
        internal readonly RazorManager manager_;
        protected readonly string build_file_path_;

        internal RazorTemplateRunner(
            BuildContext context,
            string file_path,
            RazorManager manager)
        {
            this.context_ = context;
            this.file_path_ = file_path;
            this.manager_ = manager;

            this.build_file_path_ = this.context_.GlobalContext.get_build_path(file_path) + ".dll";
        }

        internal protected string process(Action<RazorRuntimeBase> initer = null)
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
            var script_file = System.IO.Path.Combine(this.context_.SourceFolder, this.file_path_);
            var script = templates.Load<RazorRuntimeBase>(
                script_file,
                this.build_file_path_,
                options);
            if (null != initer)
            {
                initer(script);
            }

            script.Context = this.context_;
            script.Parameters = this.Parameters;
            script.Manager = this.manager_;
            try
            {
                return script.Execute().Result;
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message} at processing {script_file}", ex);
            }
        }
    }
}