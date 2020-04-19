using IVySoft.VPlatform.TemplateService.Runtime.IndexScript;
using System;
using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateService.Razor
{
    internal class RazorManager : IRazorManager, IBuildContextDependent
    {
        private BuildContext context_;

        public RazorManager(IRazorRegistrator registrator)
        {
            this.registrator_ = registrator;
        }


        private readonly IRazorRegistrator registrator_;

        public void add_component(string name, string file_path)
        {
            this.registrator_.add_component(name, new RazorRegistrator.Component { FilePath = file_path, Context = this.context_ });
        }

        public RazorTemplateInstance load(string file_path)
        {
            return new RazorTemplateInstance(this.context_, file_path, this);
        }

        public void SetBuildContext(BuildContext context)
        {
            this.context_ = context;
        }

        internal RazorRegistrator.Component get_component(string name)
        {
            return this.registrator_.get_component(name);
        }

        public void ContextCompleted()
        {
        }
    }
}