using CommandLine;

namespace IVySoft.VPlatform.TemplateEngine.Cmd
{
    [Verb("build", HelpText = "Generate structure from template.")]
    internal class BuildOptions
    {
        [Option('s', "source", Required = true, HelpText = "Source folder")]
        public string Source { get; set; }

        [Option('t', "target", Required = false, HelpText = "Target file")]
        public string Target { get; set; }
        
        [Option('m', "modules", Required = false, HelpText = "Modules path")]
        public string Modules { get; set; }

    }

}