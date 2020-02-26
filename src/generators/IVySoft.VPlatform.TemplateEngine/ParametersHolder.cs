using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.TemplateEngine
{
    public class ParametersHolder
    {
        private readonly Dictionary<string, object> parameters_ = new Dictionary<string, object>();

        protected Dictionary<string, object> Parameters { get => parameters_; }

        protected void with_parameter(string name, object value)
        {
            this.parameters_.Add(name, value);
        }
    }
}
