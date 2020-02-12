using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateEngine.IndexScript
{
    public abstract class TemplateInstanceBase : TextTemplateBase
    {
        public string Layout { get; set; }

        public Dictionary<string, object> Parameters = new Dictionary<string, object>();

        protected void SetParameter(string name, string value)
        {
            this.Parameters.Add(name, value);
        }

    }
}