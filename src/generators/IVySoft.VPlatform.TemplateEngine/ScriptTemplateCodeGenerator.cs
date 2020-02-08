using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.TemplateEngine
{
    public class ScriptTemplateCodeGenerator : ITemplateCodeGenerator
    {
        private readonly TemplateCodeGeneratorOptions options_;

        public ScriptTemplateCodeGenerator(TemplateCodeGeneratorOptions options)
        {
            this.options_ = options;
        }

        public string GenerateCode(string templateFile)
        {
            return "public class " + this.options_.TemplateTypeName + " : IVySoft.VPlatform.TemplateEngine.IndexScript.IndexScriptBase { public override void Execute() { \n"
                + "#line 1 \"" + templateFile + "\"\n"
                + System.IO.File.ReadAllText(templateFile)
                + "}}";
        }

        public string GetFilePath(string templateFile)
        {
            return System.IO.Path.Combine(this.options_.RootPath, templateFile);
        }
    }
}
