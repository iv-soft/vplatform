using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.TemplateEngine
{
    public static class PathUtils
    {
        public static string AddPathSeparator(string path)
        {
            if (path.Length > 0)
            {
                switch (path[path.Length - 1])
                {
                    case '/':
                    case '\\':
                        return path;
                }

                return path + System.IO.Path.DirectorySeparatorChar;
            }

            return path;
        }
        public static string RemovePathSeparator(string path)
        {
            if (path.Length > 0)
            {
                switch (path[path.Length - 1])
                {
                    case '/':
                    case '\\':
                        return path.Substring(0, path.Length - 1);
                }
            }

            return path;
        }


        public static string RelativePath(string base_path, string path)
        {
            var b = AddPathSeparator(base_path);
            if (!path.StartsWith(b))
            {
                return null;
            }

            return path.Substring(b.Length);
        }
    }
}
