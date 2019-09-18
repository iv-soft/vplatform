using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace IVySoft.VPlatform.TemplateEngine
{
    public class Templates
    {
        private readonly TemplateCodeGenerator generator_;
        private readonly TemplateCompiler compiler_;

        public Templates(string rootPath, Action<TemplateContext> options = null)
        {
            var context = new TemplateContext();
            if (options != null)
            {
                options(context);
            }
            this.generator_ = new TemplateCodeGenerator(rootPath);
            this.compiler_ = new TemplateCompiler(rootPath, context.CompilerOptions);
        }

        public T Load<T>(string templateFile)
        {
            var asm = this.compiler_.LoadTemplate(this.generator_, templateFile);
            return (T)Activator.CreateInstance(asm.GetType("Razor.Template"));
        }
    }
}
