using CommandLine;
using System;
using System.IO;

namespace IVySoft.VPlatform.TemplateEngine.Cmd
{
    class Program
    {
        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<GenerateOptions>(args)
                .MapResult(
                  (GenerateOptions opts) => RunAddAndReturnExitCode(opts),
                  errs => 1);
        }

        private static int RunAddAndReturnExitCode(GenerateOptions opts)
        {
            var generator = new TemplateCodeGenerator(
                new TemplateCodeGeneratorOptions
                {
                    RootPath = Path.GetDirectoryName(opts.Source),
                    TemplateTypeName = opts.Name
                });
            File.WriteAllText(opts.Target, generator.GenerateCode(Path.GetFileName(opts.Source)));
            return 0;
        }
    }
}
