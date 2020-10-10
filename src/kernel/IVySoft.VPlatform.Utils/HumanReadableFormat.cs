using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace IVySoft.VPlatform.Utils
{
    public static class HumanReadableFormat
    {
        static string[] sizes = { "", "K", "M", "G", "T" };
        // Returns the human-readable file size for an arbitrary, 64-bit file size 
        // The default format is "0.### XB", e.g. "4.2 KB" or "1.434 GB"
        public static string GetBytesReadable(long len)
        {
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            return String.Format("{0:0.##} {1}", len, sizes[order]);
        }
        public static long Parse(string len)
        {
            for(int order = sizes.Length; order > 0; --order)
            {
                if(len.EndsWith(sizes[order - 1]))
                {
                    return (long)(double.Parse(len.Substring(0, len.Length - sizes[order - 1].Length), CultureInfo.InvariantCulture) * Math.Pow(1024, order - 1));

                }
            }

            throw new Exception($"Invalid lenght format {len}");
        }

        public static string LongString(string data)
        {
            if(data.Length > 1000)
            {
                return data.Substring(0, 100) + "...[" + (data.Length - 200) + " symbols skipped]..." + data.Substring(data.Length - 100);
            }
            else
            {
                return data;
            }
        }
    }
}
