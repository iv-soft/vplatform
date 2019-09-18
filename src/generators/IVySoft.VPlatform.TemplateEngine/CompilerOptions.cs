using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace IVySoft.VPlatform.TemplateEngine
{
    public class CompilerOptions
    {
        public List<MetadataReference> References { get; set; } = new List<MetadataReference>();
    }
}