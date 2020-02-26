using IVySoft.VPlatform.TemplateEngine;
using System;

namespace IVySoft.VPlatform.TemplateService.Runtime.IndexScript
{
    public class IndexScriptActionRunner : ParametersHolder
    {
        private readonly Action<IndexScriptActionContext> context_;

        public IndexScriptActionRunner(Action<IndexScriptActionContext> context)
        {
            this.context_ = context;
        }

        public new IndexScriptActionRunner with_parameter(string name, object value)
        {
            base.with_parameter(name, value);
            return this;
        }

        public void execute()
        {
            var x = new IndexScriptActionContext(this.Parameters);
            this.context_(x);
        }

    }
}