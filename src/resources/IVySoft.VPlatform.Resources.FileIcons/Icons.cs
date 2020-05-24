using System;
using System.Globalization;
using System.Resources;

namespace IVySoft.VPlatform.Resources.FileIcons
{
    public static class Icons
    {
        public static byte[] FileExt2SVG(string name, string type = "classic")
        {
            return (byte[])Resources.ResourceManager.GetObject(type + "_" + name.TrimStart('.'), Resources.Culture);
        }
    }
}
