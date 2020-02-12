using System;
using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateEngine.IndexScript
{
    public class BuildContext
    {
        public string SourceFolder { get; set; }
        public string TargetFolder { get; set; }
        public string BuildFolder { get; set; }

        private Dictionary<string, ITemplateDescription> templates = new Dictionary<string, ITemplateDescription>();

        internal void ImportModule(string module_name)
        {
            this.AddDirectory(System.IO.Path.Combine("v_modules", module_name));
        }
        internal void AddDirectory(string folder_name)
        {
            var context = new IndexScriptContext
            {
                CurrentFolder = folder_name,
                Context = this
            };

            new DirectoryBuildContext().Process(context);
        }
        public void Process()
        {
            this.AddDirectory(string.Empty);
        }

        internal ITemplateDescription AddTemplate(string name)
        {
            if (this.templates.ContainsKey(name))
            {
                throw new Exception($"Template {name} already exists");
            }

            var result = new TemplateDescription(name);
            this.templates.Add(name, result);
            return result;
        }

        internal string ProcessRazorTemplate(string layout, Dictionary<string, object> parameters)
        {
            ITemplateDescription template;
            if(!this.templates.TryGetValue(layout, out template))
            {
                throw new Exception($"Template {layout} is not exists");
            }

            return template.Execute(parameters);
        }
    }
}