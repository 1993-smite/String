using AdUserLib;
using F23.StringSimilarity;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace StrignComporation
{
    class Program
    {
        static void CompareSimilarity(string s, string s1)
        {
            Console.WriteLine($"\t{s},{s1}");
            Console.WriteLine("Damerau: {0}", s.Damerau(s1));
            Console.WriteLine("Levenshtein: {0}", s.Levenshtein(s1));
            Console.WriteLine("NormalizedLevenshtein: {0}", s.NormalizedLevenshtein(s1));
            Console.WriteLine("JaroWinkler: {0}", s.JaroWinkler(s1));
            Console.WriteLine("LongestCommonSubsequence: {0}", s.LongestCommonSubsequence(s1));
            Console.WriteLine("MetricLCS: {0}", s.MetricLCS(s1));
            Console.WriteLine("NGram(2): {0}", s.NGram(s1,2));
            Console.WriteLine("NGram(4): {0}", s.NGram(s1, 4));
            Console.WriteLine("Soundex: {0}", s.Soundex(s1));
            Console.WriteLine("Metaphone: {0}", s.Metaphone(s1));
            Console.WriteLine("DoubleMetaphone: {0}", s.DoubleMetaphone(s1));
        }

        static void Main(string[] args)
        {
            var l = new QGram();

            int index = 0;

            CompareSimilarity(
                "Карасева Екатерина Викторовна".Eng()
                    , "Лазарева Екатерина Викторовна".Eng());
            CompareSimilarity("Мацуков Александр Сергеевич".Eng()
                , "Жуков Александр Сергеевич".Eng());
            CompareSimilarity("Викторов Иван Викторович".Eng()
                ,"Васильев Иван Викторович".Eng());
            CompareSimilarity("Попов Александр Александрович".Eng()
                ,"Долгов Александр Александрович".Eng());
            CompareSimilarity("Сасов Александр Викторович".Eng()
                , "Сасов Александр Виктрович".Eng());
            CompareSimilarity(
                "Сасов Александр Викторович".Eng()
                , "Sasov Alex".Eng());

            Console.ReadLine();
        }
    }
}
