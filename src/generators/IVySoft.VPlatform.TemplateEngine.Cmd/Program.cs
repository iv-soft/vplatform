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
            try
            {
                var context = new Module.ModuleContext
                {
                    SourceFolder = opts.Source,
                    BuildFolder = Environment.CurrentDirectory,
                    GlobalContext = new IndexScript.GlobalContext
                    {
                        SourceFolder = opts.Source,
                        TargetFolder = string.IsNullOrWhiteSpace(opts.Target) ? Path.Combine(opts.Source, "build") : opts.Target,
                        BuildFolder = Environment.CurrentDirectory,
                        ModulesFolder = Path.Combine(opts.Source, "v_modules")
                    }
                };

                context.Process();
                return 0;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
        }
    }
}
