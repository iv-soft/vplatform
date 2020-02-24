
namespace IVySoft.VPlatform.TemplateEngine
{
    public interface ITemplateCompiler
    {
        System.Reflection.Assembly LoadTemplate(ITemplateCodeGenerator generator, string templateFile, string dllPath, CompilerOptions options);
        void Compile(string code, string dllPath, CompilerOptions options);
    }
}
