using Phonix;
using System;

namespace StrignComporation
{
    public static class StringExtensionPhonix
    {
        public static double Soundex(this string s, string s1)
        {
            var _generator = new Soundex();

            return TemplateD(_generator.IsSimilar, s, s1);
        }

        private static double TemplateD(Func<string[], bool> func, string s, string s1)
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

            foreach (var it in minArr)
            {
                min = 1;
                for (int i = 0; i < maxArr.Length; i++)
                {
                    cur = func(new string[2] { it, maxArr[i] });

                    if (cur)
                    {
                        min = 0;
                        break;
                    }
                }
                d += min;
            }
            return d;
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

    }
}
