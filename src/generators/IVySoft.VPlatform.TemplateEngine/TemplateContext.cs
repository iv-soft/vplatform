using System;
using Microsoft.CodeAnalysis;

namespace IVySoft.VPlatform.TemplateEngine
{
    public class TemplateContext
    {
        public CompilerOptions CompilerOptions { get; set; } = new CompilerOptions();
    }
}