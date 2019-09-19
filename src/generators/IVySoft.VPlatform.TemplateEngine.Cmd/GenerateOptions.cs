using CommandLine;

namespace IVySoft.VPlatform.TemplateEngine.Cmd
{
    [Verb("generate", HelpText = "Generate code from template.")]
    internal class GenerateOptions
    {
        [Option('s', "source", Required = true, HelpText = "Source file")]
        public string Source { get; set; }

        [Option('t', "target", Required = true, HelpText = "Target file")]
        public string Target { get; set; }

        [Option('n', "name", Required = false, HelpText = "Template name")]
        public string Name { get; set; }
    }
}