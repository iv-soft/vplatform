using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.TemplateEngine
{
    public abstract class TextTemplateWithModel<T> : TextTemplateBase
    {
        public T Model { get; set; }
    }
}
