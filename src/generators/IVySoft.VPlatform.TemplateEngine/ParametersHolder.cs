using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.TemplateEngine
{
    public class ParametersHolder
    {
        private readonly Dictionary<string, object> parameters_ = new Dictionary<string, object>();

        protected Dictionary<string, object> Parameters { get => parameters_; }

        protected void with(string name, object value)
        {
            this.parameters_.Add(name, value);
        }

        protected bool contains(string name)
        {
            return this.parameters_.ContainsKey(name);
        }
    }
}
