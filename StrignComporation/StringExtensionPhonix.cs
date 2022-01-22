using Phonix;
using System;
using System.Collections.Generic;

namespace StrignComporation
{
    public static class StringExtensionPhonix
    {
        public static double Soundex(this string s, string s1)
        {
            var _generator = new Soundex();

            return TemplateD(_generator.IsSimilar, s, s1);
        }

        /// <summary>
        /// only one word
        /// </summary>
        /// <param name="func"></param>
        /// <param name="s"></param>
        /// <param name="s1"></param>
        /// <returns></returns>
        private static double TemplateD(Func<string[], bool> func, string s, string s1, bool onlyOneWord = true)
        {
            var sArr = s.Split(" ");
            var sArr1 = s1.Split(" ");

            double d = 0;
            double min;
            int minIndex = 0;
            bool cur;

            bool sLowerS1 = sArr.Length <= sArr1.Length;

            var minArr = sLowerS1
                ? sArr
                : sArr1;
            var maxArr = sLowerS1
                ? sArr1
                : sArr;

            var lasted = new List<int>();

            foreach (var it in minArr)
            {
                min = 1;

                if (it.Length < 3)
                {
                    continue;
                }

                for (int i = 0; i < maxArr.Length; i++)
                {
                    if (lasted.Contains(i) || maxArr[i].Length < 3)
                        continue;

                    cur = func(new string[2] { it, maxArr[i] });

                    if (cur)
                    {
                        if (onlyOneWord)
                            lasted.Add(i);
                        min = 0;
                        break;
                    }
                }
                d += min;
            }
            return d;
        }

        public static string Filter(this string s)
        {
            return s.Replace(".","")
                .Replace(",", "")
                .Replace("-", " ");
        }

        public static double Metaphone(this string s, string s1)
        {
            var _generator = new Metaphone();

            return TemplateD(_generator.IsSimilar, s, s1);
        }

        public static double DoubleMetaphone(this string s, string s1)
        {
            var _generator = new DoubleMetaphone();

            return TemplateD(_generator.IsSimilar, s, s1);
        }

        public static double MatchRatingApproach(this string s, string s1)
        {
            var _generator = new MatchRatingApproach();

            return TemplateD(_generator.IsSimilar, s, s1);
        }

        public static double Caverphone(this string s, string s1)
        {
            var _generator = new CaverPhone();

            return TemplateD(_generator.IsSimilar, s, s1);
        }

    }
}
