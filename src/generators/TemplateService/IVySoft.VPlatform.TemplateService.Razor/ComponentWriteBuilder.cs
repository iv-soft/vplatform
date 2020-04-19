using IVySoft.VPlatform.TemplateEngine;
using IVySoft.VPlatform.TemplateService.Runtime.IndexScript;

namespace IVySoft.VPlatform.TemplateService.Razor
{
    public class ComponentWriteBuilder : RazorTemplateRunner, ITextTemplateWriter
    {
        private RazorRegistrator.Component component_;

        internal ComponentWriteBuilder(
            RazorRegistrator.Component component,
            RazorManager manager,
            System.Collections.Generic.Dictionary<string, object> parameters)
            : base(component.Context, component.FilePath, manager)
        {
            this.component_ = component;
            if(null != parameters)
            {
                foreach(var item in parameters)
                {
                    base.with(item.Key, item.Value);
                }
            }
        }
        public new ComponentWriteBuilder with(string name, object value)
        {
            base.with(name, value);
            return this;
        }

        public string Render(TextTemplateBase context)
        {
            var runtime = context as RazorRuntimeBase;
            foreach(var item in runtime.Parameters)
            {
                this.with(item.Key, item.Value);
            }

            return base.process(null);
        }
    }
}