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

        protected EntityModel.EntityType add_entity_type(string name, string ns)
        {
            return this.Context.Context.GlobalContext.AddEntityType(name, ns);
        }

        protected void create_collection(string name, EntityModel.EntityType itemType)
        {

        }
    }
}
