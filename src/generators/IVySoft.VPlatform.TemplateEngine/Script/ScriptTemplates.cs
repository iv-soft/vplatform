using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IVySoft.VPlatform.TemplateEngine.Script
{
    public class ScriptTemplates
    {
        private readonly ScriptTemplateCodeGenerator generator_;
        private readonly TemplateCompiler compiler_;
        private readonly TemplateCodeGeneratorOptions generatorOptions_;

        public TemplateCodeGeneratorOptions GeneratorOptions { get => generatorOptions_; }

        public ScriptTemplates(
            TemplateCodeGeneratorOptions generatorOptions,
            Action<TemplateContext> options = null)
        {
            var context = new TemplateContext();
            if (options != null)
            {
                options(context);
            }
            this.generator_ = new ScriptTemplateCodeGenerator(generatorOptions);
            this.compiler_ = new TemplateCompiler(generatorOptions.TempPath, context.CompilerOptions);
            this.generatorOptions_ = generatorOptions;
        }

        public T Load<T>(string templateFile)
        {
            var asm = this.compiler_.LoadTemplate(this.generator_, templateFile);
            return (T)Activator.CreateInstance(asm.GetType(
                (string.IsNullOrWhiteSpace(this.generatorOptions_.TemplateTypeName) ? "Template" : this.generatorOptions_.TemplateTypeName)));
        }
        public Assembly Compile(string code, string dllPath)
        {
            this.compiler_.Compile(code, dllPath);
            return Assembly.LoadFile(dllPath);
        }
    }
}
