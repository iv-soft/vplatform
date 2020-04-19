using System;
using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateService.Runtime.IndexScript
{
    public class GlobalContext
    {
        public string SourceFolder { get; set; }
        public string TargetFolder { get; set; }
        public string BuildFolder { get; set; }
        public string ModulesFolder { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
        public List<Microsoft.CodeAnalysis.MetadataReference> References { get; set; }

        private Dictionary<string, ModuleCompile> modules = new Dictionary<string, ModuleCompile>();
        private Dictionary<string, object> variables = new Dictionary<string, object>();
        private readonly Dictionary<string, Action<IndexScriptActionContext>> actions_ = new Dictionary<string, Action<IndexScriptActionContext>>();
        
        private readonly Dictionary<string, string> build_paths_ = new Dictionary<string, string>();

        internal ModuleCompile ImportModule(string module_name)
        {
            ModuleCompile result;
            if (!this.modules.TryGetValue(module_name, out result))
            {
                var context = new BuildContext
                {
                    SourceFolder = System.IO.Path.Combine(this.ModulesFolder, module_name),
                    BuildFolder = System.IO.Path.Combine(this.BuildFolder, "modules", module_name),
                    GlobalContext = this
                };

                this.add_build_path(context.SourceFolder, context.BuildFolder);

                result = new ModuleCompile { Name = module_name, Context = context };
                this.modules.Add(module_name, result);

                context.Process();
            }
            return result;
        }

        internal bool TryGetVariable(string name, out object value)
        {
            return this.variables.TryGetValue(name, out value);
        }

        public void SetVariable(string name, object value)
        {
            this.variables[name] = value;
        }

        public void add_action(string name, Action<IndexScriptActionContext> action)
        {
            this.actions_.Add(name, action);
        }

        public IndexScriptActionRunner action(IndexScriptBase context, string name)
        {
            Action<IndexScriptActionContext> callback;
            if (!this.actions_.TryGetValue(name, out callback))
            {
                throw new Exception($"Action {name} not found");
            }

            return new IndexScriptActionRunner(context, callback);
        }

        public void add_build_path(string source_path, string build_path)
        {
            this.build_paths_.Add(source_path, build_path);
        }

        public string get_build_path(string source_path)
        {
            foreach(var item in this.build_paths_)
            {
                var result = TemplateEngine.PathUtils.RelativePath(item.Key, source_path);
                if (null != result)
                {
                    return System.IO.Path.Combine(item.Value, result);
                }
            }

            throw new System.Exception($"File {source_path} not in modules or source path");
        }
    }
}