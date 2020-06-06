using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Generic
{
    public static class CollectionUtils
    {
        public static int SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            int result = 0;
            foreach(var item in source){
                result += selector(item);
            }
            return result;
        }
        public static long SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            long result = 0;
            foreach (var item in source)
            {
                result += selector(item);
            }
            return result;
        }

        public static int AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            int result = 0;
            int count = 0;
            foreach (var item in source)
            {
                result += selector(item);
                ++count;
            }
            if(0 == count)
            {
                return result;
            }
            else
            {
                return result / count;
            }
        }
    }
}
