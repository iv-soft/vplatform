using IVySoft.VPlatform.TemplateEngine;

namespace IVySoft.VPlatform.TemplateService.Razor
{
    public abstract class RazorRuntimeBase : TextTemplateBase
    {
        public string Content { get; internal set; }
    }
}