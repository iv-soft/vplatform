namespace IVySoft.VPlatform.TemplateEngine.IndexScript
{
    internal class XmlTemplateBuilder : IXmlTemplateBuilder
    {
        private IndexScriptBase indexScriptBase;
        private string name;
        private string file;
        public XmlTemplateBuilder(IndexScriptBase indexScriptBase, string name, string file)
        {
            this.indexScriptBase = indexScriptBase;
            this.name = name;
            this.file = file;
        }
        public IXmlTemplateBuilder with_string_parameter(string name)
        {
            return this;
        }
        public IXmlTemplateBuilder with_xml_parameter(string name)
        {
            return this;
        }
    }
}