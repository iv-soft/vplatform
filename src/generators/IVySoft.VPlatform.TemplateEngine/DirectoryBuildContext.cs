using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.TemplateEngine
{
    public class DirectoryBuildContext
    {
        public string RootFolder { get; set; }
        public string TargetFolder { get; set; }
        public CompilerOptions CompilerOptions { get; set; } = new CompilerOptions();

        public int Process()
        {
            var index_file = System.IO.Path.Combine(this.RootFolder, "index.vgen");
            if (!System.IO.File.Exists(index_file))
            {
                throw new Exception($"File {index_file} not found");
            }

            var generatorOptions = new TemplateCodeGeneratorOptions
            {
                RootPath = this.RootFolder,
                TemplateTypeName = "IndexScript"
            };
            var generator = new ScriptTemplateCodeGenerator(generatorOptions);
            var options = new CompilerOptions
            {
                References = new List<Microsoft.CodeAnalysis.MetadataReference>(this.CompilerOptions.References)
            };
            options.References.Add(MetadataReference.CreateFromFile(typeof(IndexScript.IndexScriptBase).Assembly.Location));
            var compiler = new TemplateCompiler(this.RootFolder, options);
            var asm = compiler.LoadTemplate(generator, index_file);
            var script = (IndexScript.IndexScriptBase)Activator.CreateInstance(
                asm.GetType(generatorOptions.TemplateTypeName));
            script.Execute();

            return 0;
        }
    }
}
