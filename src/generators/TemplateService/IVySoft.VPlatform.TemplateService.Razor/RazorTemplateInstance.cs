using IVySoft.VPlatform.TemplateEngine;
using IVySoft.VPlatform.TemplateEngine.Razor;
using IVySoft.VPlatform.TemplateService.Runtime.IndexScript;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateService.Razor
{
    public class RazorTemplateInstance : RazorTemplateRunner
    {
        internal RazorTemplateInstance(
            BuildContext context,
            string file_path,
            RazorManager manager)
            : base(context, file_path, manager)
        {
        }

        public new RazorTemplateInstance with(string name, object value)
        {
            base.with(name, value);
            return this;
        }

        public void process(string output_path)
        {
            this.process(string.Empty, output_path);
        }

        public void process(string file_name, string output_path)
        {
            var body = base.process(script =>
            {
                if (!string.IsNullOrWhiteSpace(file_name))
                {
                    script.Content = System.IO.File.ReadAllText(
                        System.IO.Path.Combine(this.context_.SourceFolder, file_name));
                }
            });
            var dest_path = System.IO.Path.Combine(
                this.context_.GlobalContext.TargetFolder, output_path);
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(dest_path));
            System.IO.File.WriteAllText(dest_path, body);
        }

        public void process(RazorTemplateInstance input_code, string output_path)
        {
            var content = ((RazorTemplateRunner)input_code).process(null);
            var body = base.process(script =>
            {
                script.Content = content;
            });
            var dest_path = System.IO.Path.Combine(
                this.context_.GlobalContext.TargetFolder, output_path);
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(dest_path));
            System.IO.File.WriteAllText(dest_path, body);
        }
    }
}