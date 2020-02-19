using IVySoft.VPlatform.TemplateEngine;
using IVySoft.VPlatform.TemplateEngine.Razor;
using IVySoft.VPlatform.TemplateService.Runtime.IndexScript;
using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateService.Razor
{
    public class RazorTemplateInstance
    {
        private readonly Dictionary<string, object> parameters = new Dictionary<string, object>();
        private readonly BuildContext context_;
        private readonly string file_path_;

        public RazorTemplateInstance(BuildContext context, string file_path)
        {
            this.context_ = context;
            this.file_path_ = file_path;
        }

        public RazorTemplateInstance with_parameter(string name, object value)
        {
            this.parameters.Add(name, value);
            return this;
        }

        public void process(string file_name, string output_path)
        {
            var templates = new RazorTemplates(new TemplateCodeGeneratorOptions
            {
                RootPath = this.context_.SourceFolder,
                TempPath = this.context_.BuildFolder,
                TemplateTypeName = "TemplateRuntime",
                BaseType = typeof(RazorRuntimeBase).FullName
            },
            options =>
            {
                options.CompilerOptions.References.AddRange(this.context_.GlobalContext.References);
            });

            var script = templates.Load<RazorRuntimeBase>(System.IO.Path.Combine(this.context_.SourceFolder, this.file_path_));
            script.Content = System.IO.File.ReadAllText(System.IO.Path.Combine(this.context_.SourceFolder, file_name));

            var body = script.Execute().Result;
            System.IO.File.WriteAllText(System.IO.Path.Combine(this.context_.GlobalContext.TargetFolder), body);
        }
    }
}