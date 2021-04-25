using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.Utils
{
    public static class CSharpUtils
    {
        public static string TypeShortName(string type_name)
        {
            switch (type_name)
            {
                case "System.Boolean": return "bool";
                case "System.String": return "string";
                case "System.Int32": return "int";
                default: return type_name;
            }
        }
    }
}
