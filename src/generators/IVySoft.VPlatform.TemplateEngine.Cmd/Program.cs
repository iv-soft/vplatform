using CommandLine;
using System;
using System.IO;

namespace IVySoft.VPlatform.TemplateEngine.Cmd
{
    class Program
    {
        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<GenerateOptions, BuildOptions>(args)
                .MapResult(
                  (GenerateOptions opts) => RunAddAndReturnExitCode(opts),
                  (BuildOptions opts) => RunAddAndReturnExitCode(opts),
                  errs => 1);
        }

        private static int RunAddAndReturnExitCode(GenerateOptions opts)
        {
            var generator = new RazorTemplateCodeGenerator(
                new TemplateCodeGeneratorOptions
                {
                    RootPath = Path.GetDirectoryName(opts.Source),
                    TemplateTypeName = opts.Name
                });
            File.WriteAllText(opts.Target, generator.GenerateCode(Path.GetFileName(opts.Source)));
            return 0;
        }
        private static int RunAddAndReturnExitCode(BuildOptions opts)
        {
            var context = new DirectoryBuildContext();
            context.RootFolder = opts.Source;
            context.TargetFolder = string.IsNullOrWhiteSpace(opts.Target) ? Environment.CurrentDirectory : opts.Target;

            return context.Process();
        }
    }
}
