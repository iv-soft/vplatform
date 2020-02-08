namespace IVySoft.VPlatform.TemplateEngine
{
    public interface ITemplateCodeGenerator
    {
        string GenerateCode(string templateFile);
        string GetFilePath(string templateFile);
    }
}