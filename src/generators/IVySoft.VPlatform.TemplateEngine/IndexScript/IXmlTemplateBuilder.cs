namespace IVySoft.VPlatform.TemplateEngine.IndexScript
{
    public interface IXmlTemplateBuilder
    {
        IXmlTemplateBuilder with_string_parameter(string name);
        IXmlTemplateBuilder with_xml_parameter(string name);
    }
}