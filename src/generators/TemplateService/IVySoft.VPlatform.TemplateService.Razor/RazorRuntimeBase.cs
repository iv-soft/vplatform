using IVySoft.VPlatform.TemplateEngine;
using IVySoft.VPlatform.TemplateService.Runtime.IndexScript;
using System;
using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateService.Razor
{
    public abstract class RazorRuntimeBase : TextTemplateBase
    {
        public string Content { get; internal set; }
        public BuildContext Context { get; set; }

        public Dictionary<string, object> Parameters { get; internal set; }

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

    }
}