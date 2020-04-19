using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.TemplateService.Razor
{
    public interface IRazorManager
    {
        RazorTemplateInstance load(string file_path);
        void add_component(string name, string file_path);
    }
}
