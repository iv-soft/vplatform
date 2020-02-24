using System;
using System.Reflection;

namespace IVySoft.VPlatform.TemplateEngine.Script
{
    public class ScriptTemplates
    {
        private readonly ScriptTemplateCodeGenerator generator_;
        private readonly ITemplateCompiler compiler_;
        private readonly TemplateCodeGeneratorOptions generatorOptions_;

        public TemplateCodeGeneratorOptions GeneratorOptions { get => generatorOptions_; }

        public ScriptTemplates(
            ITemplateCompiler compiler,
            TemplateCodeGeneratorOptions generatorOptions)
        {
            this.generator_ = new ScriptTemplateCodeGenerator(generatorOptions);
            this.compiler_ = compiler;
            this.generatorOptions_ = generatorOptions;
        }

        public T Load<T>(string templateFile, string dllPath, CompilerOptions options)
        {
            var asm = this.compiler_.LoadTemplate(this.generator_, templateFile, dllPath, options);
            return (T)Activator.CreateInstance(asm.GetType(
                (string.IsNullOrWhiteSpace(this.generatorOptions_.TemplateTypeName) ? "Template" : this.generatorOptions_.TemplateTypeName)));
        }
        public Assembly Compile(string code, string dllPath, CompilerOptions options)
        {
            this.compiler_.Compile(code, dllPath, options);
            return System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath);
        }
    }
}
