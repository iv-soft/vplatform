using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace IVySoft.VPlatform.TemplateEngine
{
    public class TemplateCompiler
    {
        private readonly string tmpPath_;
        private readonly CompilerOptions options_;
        private readonly Dictionary<string, Assembly> loaded_assemblies_ = new Dictionary<string, Assembly>();

        public TemplateCompiler(string tmpPath, CompilerOptions options)
        {
            this.tmpPath_ = tmpPath;
            this.options_ = options;
        }

        public Assembly LoadTemplate(TemplateCodeGenerator generator, string templateFile)
        {
            var dllPath = this.CompileTemplate(generator, templateFile);
            Assembly result;
            if(!this.loaded_assemblies_.TryGetValue(templateFile, out result))
            {
                result = Assembly.LoadFile(dllPath);
            }

            return result;
        }

        public string CompileTemplate(TemplateCodeGenerator generator, string templateFile)
        {
            var dllPath = Path.Combine(this.tmpPath_, templateFile + ".dll");
            if(!File.Exists(dllPath) || new FileInfo(dllPath).LastWriteTime < new FileInfo(generator.GetFilePath(templateFile)).LastWriteTime)
            {
                this.loaded_assemblies_.Remove(templateFile);
                Compile(generator.GenerateCode(templateFile), dllPath);
            }
            return dllPath;
        }

        public void Compile(string code, string dllPath)
        {
            var tree = CSharpSyntaxTree.ParseText(code);

            var compilation = CSharpCompilation.Create(
                Path.GetFileNameWithoutExtension(dllPath),
                new[] { tree },
                this.options_.References.Concat(
                new[]
                {
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location), // include corlib
                    MetadataReference.CreateFromFile(typeof(RazorCompiledItemAttribute).Assembly.Location), // include Microsoft.AspNetCore.Razor.Runtime
                    MetadataReference.CreateFromFile(Assembly.GetExecutingAssembly().Location), // this file (that contains the MyTemplate base class)
                    MetadataReference.CreateFromFile(typeof(TemplateCompiler).Assembly.Location),

                    MetadataReference.CreateFromFile(Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location), "System.Runtime.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location), "netstandard.dll"))
                }),
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            var result = compilation.Emit(dllPath);
            if (!result.Success)
            {
                File.Delete(dllPath);
                throw new Exception(string.Join(Environment.NewLine, result.Diagnostics));
            }
        }
    }
}
