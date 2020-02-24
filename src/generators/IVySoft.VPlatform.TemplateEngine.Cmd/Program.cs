using CommandLine;
using IVySoft.VPlatform.TemplateEngine.Razor;
using IVySoft.VPlatform.TemplateService.Entity;
using IVySoft.VPlatform.TemplateService.Runtime;
using IVySoft.VPlatform.TemplateService.Razor;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
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
                var services = new ServiceCollection();
                services.UseTemplateServiceRuntime();
                services.UseTemplateServiceEntity();
                services.UseTemplateServiceRazor();
                services.AddSingleton<ITemplateCompiler, TemplateCompiler>();

                var context = new TemplateService.Runtime.IndexScript.ModuleContext
                {
                    SourceFolder = opts.Source,
                    BuildFolder = Environment.CurrentDirectory,
                    GlobalContext = new TemplateService.Runtime.IndexScript.GlobalContext
                    {
                        SourceFolder = opts.Source,
                        TargetFolder = string.IsNullOrWhiteSpace(opts.Target) ? Path.Combine(opts.Source, "build") : opts.Target,
                        BuildFolder = Environment.CurrentDirectory,
                        ModulesFolder = Path.Combine(opts.Source, "v_modules"),
                        ServiceProvider = services.BuildServiceProvider(),
                        References = new System.Collections.Generic.List<Microsoft.CodeAnalysis.MetadataReference>
                        (
                            new Microsoft.CodeAnalysis.MetadataReference[]
                            {
                                MetadataReference.CreateFromFile(typeof(TemplateService.Runtime.IndexScript.BuildContext).Assembly.Location),
                                MetadataReference.CreateFromFile(typeof(IEntityManager).Assembly.Location),
                                MetadataReference.CreateFromFile(typeof(IRazorManger).Assembly.Location)
                            }
                        )
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
