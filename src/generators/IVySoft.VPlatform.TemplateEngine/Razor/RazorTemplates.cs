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
        private readonly ITemplateCompiler compiler_;
        private readonly TemplateCodeGeneratorOptions generatorOptions_;

        public RazorTemplates(
            ITemplateCompiler compiler,
            TemplateCodeGeneratorOptions generatorOptions)
        {
            this.compiler_ = compiler;
            this.generator_ = new RazorTemplateCodeGenerator(generatorOptions);
            this.generatorOptions_ = generatorOptions;
        }

        public T Load<T>(string templateFile, string dllPath, CompilerOptions options)
        {
            var asm = this.compiler_.LoadTemplate(this.generator_, templateFile, dllPath, options);
            return (T)Activator.CreateInstance(asm.GetType(
                "Razor." + (string.IsNullOrWhiteSpace(this.generatorOptions_.TemplateTypeName) ? "Template" : this.generatorOptions_.TemplateTypeName)));
        }
        public Assembly Compile(string code, string dllPath, CompilerOptions options)
        {
            this.compiler_.Compile(code, dllPath, options);
            return System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath);
        }
    }
}
