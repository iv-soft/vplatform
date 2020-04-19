namespace IVySoft.VPlatform.TemplateEngine
{
    public interface ITextTemplateWriter
    {
        string Render(TextTemplateBase context);
    }
}