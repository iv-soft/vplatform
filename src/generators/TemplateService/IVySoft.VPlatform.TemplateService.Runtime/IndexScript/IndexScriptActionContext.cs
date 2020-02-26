using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateService.Runtime.IndexScript
{
    public class IndexScriptActionContext
    {
        private readonly Dictionary<string, object> parameters_;

        public IndexScriptActionContext(Dictionary<string, object> parameters)
        {
            this.parameters_ = parameters;
        }

        public object get_parameter(string name)
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