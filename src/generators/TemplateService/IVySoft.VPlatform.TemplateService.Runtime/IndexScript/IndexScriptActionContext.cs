using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateService.Runtime.IndexScript
{
    public class IndexScriptActionContext
    {
        private readonly Dictionary<string, object> parameters_;
        private readonly IndexScriptBase context_;

        public IndexScriptBase Context { get => this.context_; }

        public IndexScriptActionContext(IndexScriptBase context, Dictionary<string, object> parameters)
        {
            this.context_ = context;
            this.parameters_ = parameters;
        }

        public object get(string name)
        {
            object result;
            if(!this.parameters_.TryGetValue(name, out result))
            {
                throw new System.Exception($"Parameter {name} not found");
            }

            return result;
        }
    }
}