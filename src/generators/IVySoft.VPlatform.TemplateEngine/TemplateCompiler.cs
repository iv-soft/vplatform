using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace IVySoft.VPlatform.TemplateEngine
{
    public class TemplateCompiler : ITemplateCompiler
    {
        private readonly Dictionary<string, Assembly> loaded_assemblies_ = new Dictionary<string, Assembly>();

        public TemplateCompiler()
        {
        }

        public Assembly LoadTemplate(ITemplateCodeGenerator generator, string templateFile, string dllPath, CompilerOptions options)
        {
            Assembly result;
            if (!this.loaded_assemblies_.TryGetValue(templateFile, out result))
            {
                this.CompileTemplate(generator, templateFile, dllPath, options);
                result = Assembly.LoadFile(dllPath);
            }

            return result;
        }

        public void CompileTemplate(
            ITemplateCodeGenerator generator,
            string templateFile,
            string dllPath,
            CompilerOptions options)
        {
            if (!File.Exists(dllPath)
                || new FileInfo(dllPath).LastWriteTime < new FileInfo(
                    generator.GetFilePath(templateFile)).LastWriteTime)
            {
                this.loaded_assemblies_.Remove(templateFile);
                Compile(generator.GenerateCode(templateFile), dllPath, options);
            }
        }

        public void Compile(string code, string dllPath, CompilerOptions options)
        {
            var tree = CSharpSyntaxTree.ParseText(code);

            var compilation = CSharpCompilation.Create(
                Convert.ToBase64String(SHA256.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(code))).Replace('/', '.'),
                new[] { tree },
                options.References.Concat(
                new[]
                {
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location), // include corlib
                    MetadataReference.CreateFromFile(typeof(RazorCompiledItemAttribute).Assembly.Location), // include Microsoft.AspNetCore.Razor.Runtime
                    MetadataReference.CreateFromFile(Assembly.GetExecutingAssembly().Location), // this file (that contains the MyTemplate base class)
                    MetadataReference.CreateFromFile(typeof(ITemplateCompiler).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location),//Linq
                    MetadataReference.CreateFromFile(typeof(System.Linq.Expressions.BinaryExpression).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.ComponentModel.IListSource).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(KeyAttribute).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Dictionary<object, object>).Assembly.Location),
                    MetadataReference.CreateFromFile(Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location), "System.Runtime.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location), "netstandard.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location), "System.Collections.dll"))
                }),
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            Directory.CreateDirectory(Path.GetDirectoryName(dllPath));
            var result = compilation.Emit(dllPath);
            if (!result.Success)
            {
                var r = new List<object>();
                File.Delete(dllPath);
                throw new Exception(string.Join(Environment.NewLine, result.Diagnostics));
            }
        }
    }
}
