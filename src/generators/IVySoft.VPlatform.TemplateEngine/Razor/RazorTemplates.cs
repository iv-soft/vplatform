using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;

namespace IVySoft.VPlatform.TemplateEngine.Razor
{
    public class RazorTemplates
    {
        private readonly RazorTemplateCodeGenerator generator_;
        private readonly TemplateCompiler compiler_;
        private readonly TemplateCodeGeneratorOptions generatorOptions_;

        public RazorTemplates(
            TemplateCodeGeneratorOptions generatorOptions,
            Action<TemplateContext> options = null)
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
        public Assembly Compile(string code, string dllPath)
        {
            this.compiler_.Compile(code, dllPath);
            return System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath);
        }
    }
}
