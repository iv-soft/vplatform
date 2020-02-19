using System.Linq;

namespace System
{
    static class TypeUtils
    {
        public static string UserFriendlyName(this Type t)
        {
            if (!t.IsGenericType)
            {
                return t.FullName;
            }

            var name = t.Name;
            var index = name.IndexOf("`");
            return $"{t.Namespace}.{name.Substring(0, index)}<{string.Join(",", t.GetGenericArguments().Select(x => x.UserFriendlyName()))}>";
        }
    }
}
