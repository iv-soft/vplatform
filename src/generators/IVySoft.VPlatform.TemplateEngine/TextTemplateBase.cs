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

        protected void Write(object obj)
        {
            this.outStream_.Write(obj);
        }
        protected void Write()
        {
        }
    }
}
