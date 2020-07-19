using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace IVySoft.VPlatform.TemplateEngine
{
    public abstract class TextTemplateBase
    {
        private StringWriter outStream_;
        //public object Context { get; set; }

        public abstract Task ExecuteAsync();

        public async Task<string> Execute()
        {
            var sb = new StringBuilder();
            this.outStream_ = new StringWriter(sb);
            await this.ExecuteAsync();
            this.outStream_.Close();

            return sb.ToString();
        }

        protected void WriteLiteral(string literal)
        {
            this.outStream_.Write(literal);
        }
        protected void Write(ITextTemplateWriter obj)
        {
            this.outStream_.Write(obj.Render(this));
        }

        protected void Write(object obj)
        {
            if (obj != null)
            {
                this.outStream_.Write(obj.ToString());
            }
        }
        protected void Write()
        {
        }

        public virtual void BeginWriteAttribute(
            string name,
            string prefix,
            int prefixOffset,
            string suffix,
            int suffixOffset,
            int attributeValuesCount)
        {
            this.outStream_.Write(' ');
            this.outStream_.Write(name);
            this.outStream_.Write("=\"");
        }
        public void WriteAttributeValue(string prefix, int prefixOffset, object value, int valueOffset, int valueLength, bool isLiteral)
        {
            this.outStream_.Write(value);
        }
        public virtual void EndWriteAttribute()
        {
            this.outStream_.Write("\"");
        }
    }
}
