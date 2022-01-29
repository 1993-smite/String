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
                , ("Сасов Александр Викторович", "Sasov Alex")
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
                    , () => {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("");
                        Console.WriteLine("  THE END!");
                     });
            

            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
