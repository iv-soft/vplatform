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
            this.Context.CopyFileOrFolder(
                file_name.Replace('/', System.IO.Path.DirectorySeparatorChar),
                target_path.Replace('/', System.IO.Path.DirectorySeparatorChar));
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

        public void add_action(string name, Action<IndexScriptActionContext> action)
        {
            this.Context.Context.GlobalContext.add_action(name, action);
        }

        public IndexScriptActionRunner action(string name)
        {
            return this.Context.Context.GlobalContext.action(name);
        }

        public string module_path(string module_name, string rel_path)
        {
            return System.IO.Path.Combine(this.Context.Context.GlobalContext.ModulesFolder, module_name, rel_path);
        }
        public string local_path(string rel_path)
        {
            return System.IO.Path.Combine(this.Context.Context.SourceFolder, rel_path);
        }
        public string target_path(string rel_path)
        {
            return System.IO.Path.Combine(this.Context.Context.GlobalContext.TargetFolder, rel_path);
        }
    }
}
