using System;
using System.IO;
using System.Xml.Serialization;

namespace IVySoft.VPlatform.Source.Xml
{
    public class XmlSource<T>
    {
        public string FilePath { get; set; }

        public T Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (Stream reader = new FileStream(this.FilePath, FileMode.Open))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
