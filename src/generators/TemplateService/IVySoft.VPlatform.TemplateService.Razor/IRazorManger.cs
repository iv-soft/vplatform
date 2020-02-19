using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.TemplateService.Razor
{
    public interface IRazorManger
    {
        RazorTemplateInstance load(string file_path);
    }
}
