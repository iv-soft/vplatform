using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace IVySoft.VPlatform.TemplateEngine
{
    public class Templates
    {
        private readonly RazorTemplateCodeGenerator generator_;
        private readonly TemplateCompiler compiler_;
        private readonly TemplateCodeGeneratorOptions generatorOptions_;

        public Templates(TemplateCodeGeneratorOptions generatorOptions, Action<TemplateContext> options = null)
        {
            var context = new TemplateContext();
            if (options != null)
            {
                options(context);
            }
            this.generator_ = new RazorTemplateCodeGenerator(generatorOptions);
            this.compiler_ = new TemplateCompiler(generatorOptions.TempPath, context.CompilerOptions);
            this.generatorOptions_ = generatorOptions;
        }

        public T Load<T>(string templateFile)
        {
            var asm = this.compiler_.LoadTemplate(this.generator_, templateFile);
            return (T)Activator.CreateInstance(asm.GetType(
                "Razor." + (string.IsNullOrWhiteSpace(this.generatorOptions_.TemplateTypeName) ? "Template" : this.generatorOptions_.TemplateTypeName)));
        }
    }
}
