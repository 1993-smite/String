using AdUserLib;
using F23.StringSimilarity;
using System;
using System.Text.RegularExpressions;

namespace StrignComporation
{
    public static class StringExtension
    {
        public static string Eng(this string text)
        {
            return EnglishTranslate.TranslateEnglish(text);
        }
    
        public static bool IsEnglish(this string text)
        {
            return Regex.IsMatch(text, "^[a-zA-Z ]*$");
        }
        public static bool IsRussian(this string text)
        {
            return Regex.IsMatch(text, "^[а-яА-Я ]*$");
        }
    }

    public static class StringExtensionF23
    {
        public static double Damerau(this string s, string s1)
        {
            var l = new Damerau();

            return TemplateD(l.Distance, s, s1);
        }

        public static double Levenshtein(this string s, string s1)
        {
            var l = new Levenshtein();
            return TemplateD(l.Distance, s, s1);
        }

        public static double NormalizedLevenshtein(this string s, string s1)
        {
            var l = new NormalizedLevenshtein();

            return TemplateD(l.Distance, s, s1);
        }

        public static double JaroWinkler(this string s, string s1)
        {
            var jw = new JaroWinkler();

            return TemplateD(jw.Distance, s, s1);
        }

        public static double LongestCommonSubsequence(this string s, string s1)
        {
            var lcs = new LongestCommonSubsequence();

            return TemplateD(lcs.Distance, s, s1);
        }

        public static double MetricLCS(this string s, string s1)
        {
            var lcs = new MetricLCS();

            return TemplateD(lcs.Distance, s, s1);
        }

        public static double NGram(this string s, string s1, int n = 2)
        {
            var ngram = new NGram(n);

            return TemplateD(ngram.Distance, s, s1);
        }

        private static double TemplateD(Func<string, string, double> func, string s, string s1)
        {
            var sArr = s.Split(" ");
            var sArr1 = s1.Split(" ");

            double d = 0;
            double min;
            int minIndex = 0;
            double cur;

            bool sLowerS1 = sArr.Length <= sArr1.Length;

            var minArr = sLowerS1
                ? sArr
                : sArr1;
            var maxArr = sLowerS1
                ? sArr1
                : sArr;

            foreach (var it in minArr)
            {
                min = double.MaxValue;
                for (int i = 0; i < maxArr.Length; i++)
                {
                    cur = func(it, maxArr[i]);

                    if (cur < min)
                    {
                        min = cur;
                        minIndex = i;
                    }
                }
                d += min;
            }
            return d;
        }

        public static double D(this string s, string s1)
        {
            var l = new NormalizedLevenshtein();

            return TemplateD(l.Distance, s, s1);
        }

    }
}
