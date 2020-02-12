using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateEngine.IndexScript
{
    internal class TemplateDescription : ITemplateDescription
    {
        private string name;
        private string file_name;
        private string build_folder;

        public CompilerOptions CompilerOptions { get; set; } = new CompilerOptions();

        public TemplateDescription(string name, string file_name, string build_folder)
        {
            this.name = name;
            this.file_name = file_name;
            this.build_folder = build_folder;
        }

        public Dictionary<string, ITemplateParameter> Parameters { get; } = new Dictionary<string, ITemplateParameter>();

        public TemplateDescription(string name)
        {
            this.name = name;
        }

        public string Execute(Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }
    }
}