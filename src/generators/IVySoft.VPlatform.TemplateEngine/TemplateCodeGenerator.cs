using System;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace IVySoft.VPlatform.TemplateEngine
{
    public class TemplateCodeGenerator
    {
        private readonly string rootPath_;
        private readonly RazorProjectFileSystem fs_;
        private readonly RazorProjectEngine engine_;

        public TemplateCodeGenerator(string rootPath)
        {
            this.rootPath_ = rootPath;
            this.fs_ = RazorProjectFileSystem.Create(rootPath);

            this.engine_ = RazorProjectEngine.Create(RazorConfiguration.Default, this.fs_, (builder) =>
            {
                InheritsDirective.Register(builder);
            });

        }
        public string GenerateCode(string templateFile)
        {
            var item = this.fs_.GetItem(templateFile);

            var codeDocument = this.engine_.Process(item);
            var cs = codeDocument.GetCSharpDocument();

            return cs.GeneratedCode;
        }

        internal string GetFilePath(string templateFile)
        {
            return System.IO.Path.Combine(this.rootPath_, templateFile);
        }
    }
}
