using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.TemplateEngine.IndexScript
{
    public abstract class IndexScriptBase
    {
        public IndexScriptContext Context { get; set; }

        public abstract void Execute();

        protected void import(string module_name)
        {
            this.Context.ImportModule(module_name);
        }

        protected void add_directory(string folder_name)
        {
            this.Context.AddDirectory(folder_name);
        }

        protected void copy(string file_name)
        {
            this.Context.CopyFileOrFolder(file_name);
        }

        protected IXmlTemplateBuilder add_xml_template(
            string name,
            string file)
        {
            return new XmlTemplateBuilder(this, name, file);
        }

        protected void process_razor_template(string file_name, string output_file)
        {
            var output_path = System.IO.Path.Combine(this.Context.Context.TargetFolder, this.Context.CurrentFolder, output_file);
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(output_path));

            var body = this.Context.ProcessRazorTemplate(file_name);
            System.IO.File.WriteAllText(output_path, body);
        }
    }
}
