using F23.StringSimilarity;
using System;
using System.Collections.Generic;

namespace StrignComporation
{
    public static class StringCompareExtension
    {
        private static double Compare(Func<string, string, double> func, string s, string s1, double minPercent = 30)
        {
            var sArr = s.Split(" ");
            var sArr1 = s1.Split(" ");

            double d = 0;
            double min;
            int minIndex = 0;
            double minVal = 0;
            double curVal;

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
                if (it.Length < 3)
                {
                    continue;
                }
                min = it.Length;
                for (int i = 0; i < maxArr.Length; i++)
                {
                    if (lasted.Contains(i) || maxArr[i].Length < 3)
                        continue;

                    curVal = func(it, maxArr[i]);

                    if (curVal < min)
                    {
                        min = curVal;
                        minIndex = i;
                    }
                }
                var percents = (min / it.Length) * 100;

                if (percents > minPercent)
                    return s.Length;

                lasted.Add(minIndex);

                d += min;
            }
            return d;
        }

        public static double LevenshteinCompare(this string s, string s1)
        {
            var l = new Levenshtein();
            return Compare(l.Distance, s, s1);
        }

        public static bool FuzzyCompare(this string s, string s1)
        {
            return s.Metaphone(s1) < 1 && s.NormalizedLevenshteinD(s1) < 0.2;
        }

    }
}
