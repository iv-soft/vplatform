using System;
using System.IO;
using IVySoft.VPlatform.TemplateEngine.Razor;
using Microsoft.CodeAnalysis;
using Xunit;

namespace IVySoft.VPlatform.TemplateEngine.Tests
{
    public class EngineTests
    {
        [Fact]
        public void Test1()
        {
            var compiler = new TemplateCompiler();
            var templates = new RazorTemplates(
                compiler,
                new TemplateCodeGeneratorOptions
                {
                    RootPath = Path.Combine(Path.GetDirectoryName(typeof(EngineTests).Assembly.Location))
                });
            var options = new CompilerOptions
            {
                References = new System.Collections.Generic.List<MetadataReference>(
                    new MetadataReference[] {
                        MetadataReference.CreateFromFile(typeof(TestTemplate).Assembly.Location)
                    })
            };
            var template = templates.Load<TestTemplate>("SampleTemplate.txt", "SampleTemplate.dll", options);
            template.Name = "world";

            Assert.Equal("Hello world!", template.Execute().Result);
        }
    }
}
