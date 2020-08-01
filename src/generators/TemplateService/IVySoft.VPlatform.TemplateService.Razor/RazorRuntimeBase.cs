using IVySoft.VPlatform.TemplateEngine;
using IVySoft.VPlatform.TemplateService.Runtime.IndexScript;
using System;
using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateService.Razor
{
    public abstract class RazorRuntimeBase : TextTemplateBase
    {
        private readonly SortedSet<string> ids_ = new SortedSet<string>();
        public string Content { get; internal set; }
        public BuildContext Context { get; set; }

        public Dictionary<string, object> Parameters { get; internal set; }
        internal RazorManager Manager { get; set; }
        protected T get_service<T>()
        {
            var result = this.Context.GlobalContext.ServiceProvider.GetService(typeof(T));
            if (null == result)
            {
                throw new Exception($"Service {typeof(T).FullName} not found");
            }

            var activator = result as IBuildContextDependent;
            if (null != activator)
            {
                activator.SetBuildContext(this.Context);
            }

            return (T)result;
        }

        protected ComponentWriteBuilder component(string name, System.Collections.Generic.Dictionary<string, object> parameters = null)
        {
            return new ComponentWriteBuilder(this.Manager.get_component(name), this.Manager, parameters);
        }

        protected string generate_id(string default_name)
        {
            if (!this.ids_.Contains(default_name))
            {
                this.ids_.Add(default_name);
                return default_name;
            }

            for(int i = 0; i < int.MaxValue; ++i)
            {
                var name = default_name + i.ToString();
                if (!this.ids_.Contains(name))
                {
                    this.ids_.Add(name);
                    return name;
                }
            }
            throw new Exception($"Unable to generate ID for name {default_name}");
        }
        protected void message(string msg)
        {
            Console.WriteLine(msg);
        }

        protected object get(string name)
        {
            object result;
            if(!this.Context.TryGetVariable("var:" + name, out result))
            {
                throw new Exception($"Variable {name} is not found");
            }
            return result;
        }

    }
}