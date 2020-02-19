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
            var templates = new RazorTemplates(
                new TemplateCodeGeneratorOptions
                {
                    RootPath = Path.Combine(Path.GetDirectoryName(typeof(EngineTests).Assembly.Location))
                },
                context => context.CompilerOptions.References.Add(MetadataReference.CreateFromFile(typeof(TestTemplate).Assembly.Location)));
            var template = templates.Load<TestTemplate>("SampleTemplate.txt");
            template.Name = "world";

            Assert.Equal("Hello world!", template.Execute().Result);
        }
    }
}
