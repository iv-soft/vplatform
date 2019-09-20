using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.Utils
{
    public class Translit
    {
        private static Dictionary<char, string> translit;

        static Translit()
        {
            translit = new Dictionary<char, string>();

            var russian = "абвгдеёжзийклмнопрстуфхцчшщыэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЫЭЮЯ";
            var expected = "a b v g d e yo zh z i y k l m n o p r s t u f kh ts ch sh shch y e yu ya A B V G D E Yo Zh Z I Y K L M N O P R S T U F Kh Ts Ch Sh Shch Y E Yu Ya".Split();
            for (int i = 0; i < russian.Length; ++i)
            {
                translit.Add(russian[i], expected[i]);
            }

            for (char ch = 'a'; ch < 'z'; ++ch)
            {
                translit.Add(ch, "" + ch);
            }

            for (char ch = 'A'; ch < 'Z'; ++ch)
            {
                translit.Add(ch, "" + ch);
            }

            for (char ch = '0'; ch < '9'; ++ch)
            {
                translit.Add(ch, "" + ch);
            }

            foreach (var ch in " _+=:,")
            {
                translit.Add(ch, "_");
            }

            translit.Add('.', ".");
        }

        public static string TranslateFolderName(string name)
        {
            var result = new StringBuilder();
            foreach (var ch in name)
            {
                string item;
                if (translit.TryGetValue(ch, out item))
                {
                    result.Append(item);
                }
            }

            return result.ToString();
        }

    }
}
