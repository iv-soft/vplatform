using IVySoft.VPlatform.TemplateEngine;
using System;

namespace IVySoft.VPlatform.TemplateService.Runtime.IndexScript
{
    public class IndexScriptActionRunner : ParametersHolder
    {
        private readonly IndexScriptBase context_;
        private readonly Action<IndexScriptActionContext> callback_;

        public IndexScriptActionRunner(IndexScriptBase context, Action<IndexScriptActionContext> callback)
        {
            this.context_ = context;
            this.callback_ = callback;
        }

        public new IndexScriptActionRunner with(string name, object value)
        {
            base.with(name, value);
            return this;
        }

        public void execute()
        {
            var x = new IndexScriptActionContext(this.context_, this.Parameters);
            this.callback_(x);
        }

    }
}