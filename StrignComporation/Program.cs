using AdUserLib;
using F23.StringSimilarity;
using NLog;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StrignComporation
{
    class Program
    {
        static void CompareWrite(string title, double val, double min = 31)
        {
            if (val < min)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine("{0}: {1}", title, val);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void CompareSimilarity(string s, string s1)
        {
            double compare;

            Console.WriteLine($"\t{s},{s1}");
            //Console.WriteLine("Damerau: {0}", s.Damerau(s1));
            Console.WriteLine("Levenshtein: {0}", s.Levenshtein(s1));
            Console.WriteLine("LevenshteinD: {0}", s.LevenshteinD(s1));

            compare = s.LevenshteinCompare(s1);
            CompareWrite("LevenshteinCompare", compare, s.Length / 3);

            compare = s.NormalizedLevenshtein(s1);
            CompareWrite("NormalizedLevenshtein", compare, 0.17);
            compare = s.NormalizedLevenshteinD(s1);
            CompareWrite("NormalizedLevenshteinD", compare, 0.20);

            compare = s.JaroWinkler(s1);
            CompareWrite("JaroWinkler", compare, 0.31);
            Console.WriteLine("JaroWinklerD: {0}", s.JaroWinklerD(s1));
            Console.WriteLine("LongestCommonSubsequence: {0}", s.LongestCommonSubsequence(s1));
            Console.WriteLine("MetricLCS: {0}", s.MetricLCS(s1));
            Console.WriteLine("NGram(2): {0}", s.NGram(s1,2));
            Console.WriteLine("NGram(4): {0}", s.NGram(s1, 4));
            Console.WriteLine("Soundex: {0}", s.Soundex(s1));
            Console.WriteLine("Metaphone: {0}", s.Metaphone(s1)); 
            //Console.WriteLine("Metaphone: {0}", s.MetaphoneS(s1));
            //Console.WriteLine("MetaphoneRU: {0}", s.MetaphoneRU(s1));
            Console.WriteLine("DoubleMetaphone: {0}", s.DoubleMetaphone(s1));
            Console.WriteLine("DoubleMetaphone: {0}", s.DoubleMetaphoneS(s1));

            Console.WriteLine("------------------------------------------------------------------------------");
        }

        static IEnumerable<(string, string)> GetTest()
        {
            var tests = new List<(string, string)>()
            {
                ("Карасева Екатерина Викторовна", "Лазарева Екатерина Викторовна")
                , ("Мацуков Александр Сергеевич", "Жуков Александр Сергеевич")
                , ("Викторов Иван Викторович", "Васильев Иван Викторович")
                , ("Попов Александр Александрович", "Долгов Александр Александрович")
                , ("Сасов Александр Викторович", "Сасов Александр Виктрович")
                , ("Сасов Александр Викторович", "Sasoev Alexander Vicktorovich")
                , ("Иванов Иван Алексеевич", "Иванов Иван Иванович")
                , ("Иванов Икар Иванович", "Иванов Иван Иванович")
            };

            foreach (var test in tests)
                yield return test;
        }

        static void Main(string[] args)
        {
            GetTest()
                .ToObservable()
                .Subscribe(
                    x => CompareSimilarity(x.Item1.Eng(), x.Item2.Eng())
                    , ex => Console.WriteLine(ex.Message)
                    , () =>
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("");
                        Console.WriteLine("  THE END!");
                    });


            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
