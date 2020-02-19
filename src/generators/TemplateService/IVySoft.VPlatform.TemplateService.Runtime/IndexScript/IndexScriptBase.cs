using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.TemplateService.Runtime.IndexScript
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
            this.copy(file_name, file_name);
        }

        protected void copy(string file_name, string target_path)
        {
            this.Context.CopyFileOrFolder(file_name, target_path);
        }

        protected T get_service<T>()
        {
            var result = this.Context.Context.GlobalContext.ServiceProvider.GetService(typeof(T));
            if(null == result)
            {
                throw new Exception($"Service {typeof(T).FullName} not found");
            }

            var activator = result as IBuildContextDependent;
            if(null != activator)
            {
                activator.SetBuildContext(this.Context.Context);
            }

            return (T)result;
        }
    }
}
