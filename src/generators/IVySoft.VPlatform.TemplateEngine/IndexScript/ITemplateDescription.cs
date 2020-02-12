using System.Collections.Generic;

namespace IVySoft.VPlatform.TemplateEngine.IndexScript
{
    internal interface ITemplateDescription
    {
        string Execute(Dictionary<string, object> parameters);

    }
}