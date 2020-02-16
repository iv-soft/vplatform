using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IVySoft.VPlatform.TemplateEngine
{
    public class DirectoryBuildContext
    {
        public CompilerOptions CompilerOptions { get; set; } = new CompilerOptions();

        public void Process(IndexScript.IndexScriptContext context)
        {
            var index_file = System.IO.Path.Combine(
                context.Context.SourceFolder,
                context.CurrentFolder,
                "index.vgen");
            if (!System.IO.File.Exists(index_file))
            {
                throw new Exception($"File {index_file} not found");
            }

            var generatorOptions = new TemplateCodeGeneratorOptions
            {
                RootPath = System.IO.Path.Combine(context.Context.SourceFolder, context.CurrentFolder),
                TempPath = System.IO.Path.Combine(context.Context.BuildFolder, context.CurrentFolder),
                TemplateTypeName = "IndexScript"
            };

            System.IO.Directory.CreateDirectory(generatorOptions.RootPath);
            System.IO.Directory.CreateDirectory(generatorOptions.TempPath);

            var generator = new ScriptTemplateCodeGenerator(generatorOptions);
            var options = new CompilerOptions
            {
                References = new List<Microsoft.CodeAnalysis.MetadataReference>(this.CompilerOptions.References)
            };
            options.References.Add(MetadataReference.CreateFromFile(typeof(IndexScript.IndexScriptBase).Assembly.Location));
            options.References.AddRange(
                context.Context.GlobalContext.EntityTypes.Select(
                    x => MetadataReference.CreateFromFile(
                        context.Context.GlobalContext.GetCLSType(x.Value).Assembly.Location)));
            var compiler = new TemplateCompiler(
                System.IO.Path.Combine(context.Context.BuildFolder, context.CurrentFolder),
                options);
            var asm = compiler.LoadTemplate(generator, index_file);
            var script = (IndexScript.IndexScriptBase)Activator.CreateInstance(
                asm.GetType(generatorOptions.TemplateTypeName));
            script.Context = context; 
            script.Execute();
        }
    }
}
