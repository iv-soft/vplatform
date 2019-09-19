using System;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace IVySoft.VPlatform.TemplateEngine
{
    public class TemplateCodeGenerator
    {
        private readonly TemplateCodeGeneratorOptions options_;
        private readonly RazorProjectFileSystem fs_;
        private readonly RazorProjectEngine engine_;

        public TemplateCodeGenerator(TemplateCodeGeneratorOptions options)
        {
            this.options_ = options;
            this.fs_ = RazorProjectFileSystem.Create(options.RootPath);

            this.engine_ = RazorProjectEngine.Create(RazorConfiguration.Default, this.fs_, (builder) =>
            {
                InheritsDirective.Register(builder);
                if (!string.IsNullOrWhiteSpace(this.options_.TemplateTypeName))
                {
                    builder.ConfigureClass((document, @class) =>
                    {
                        @class.ClassName = this.options_.TemplateTypeName;
                    });
                }
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
            return System.IO.Path.Combine(this.options_.RootPath, templateFile);
        }
    }
}
