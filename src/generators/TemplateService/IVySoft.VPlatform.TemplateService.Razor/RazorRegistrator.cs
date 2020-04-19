using IVySoft.VPlatform.TemplateService.Runtime.IndexScript;
using System;
using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateService.Razor
{
    internal class RazorRegistrator : IRazorRegistrator
    {
        internal class Component
        {
            public string FilePath { get; set; }
            public BuildContext Context { get; set; }
        }
        private readonly Dictionary<string, Component> components_ = new Dictionary<string, Component>();

        public void add_component(string name, Component component)
        {
            this.components_.Add(name, component);
        }
        public Component get_component(string name)
        {
            Component component;
            if (!this.components_.TryGetValue(name, out component))
            {
                throw new Exception($"Component {name} is not found");
            }

            return component;
        }
    }
}