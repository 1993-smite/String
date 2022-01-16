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
        public static Logger Logger =
            LogManager.Setup().GetCurrentClassLogger();

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
        }

        static void Main(string[] args)
        {
            var l = new QGram();

            int index = 0;

            //Console.WriteLine("{0}: {1}", ++index, "Иванов Иван Иванович".D("Иван Иванов"));
            //Console.WriteLine("{0}: {1}", ++index, "Петров Иван Иванович".D("Иван Иванов"));
            //Console.WriteLine("{0}: {1}", ++index, "Иванов Алексей Иванович".D("Иван Иванов"));
            //Console.WriteLine("{0}: {1}", ++index, "Сасов Александр".D("Александр Викторович Сасов"));
            //Console.WriteLine("{0}: {1}", ++index, "Сасов Александр Викторович".D("Александр Викторович Сасов"));
            //Console.WriteLine("{0}: {1}", ++index, "Сасов Виктор Васильевич".D("Александр Викторович Сасов"));
            //Console.WriteLine("{0}: {1}", ++index, "Часов Александр Викторович".D("Александр Викторович Сасов"));
            //Console.WriteLine("{0}: {1}", ++index, "Сасов Александр".D("Александр Викторович Костров"));
            //Console.WriteLine("{0}: {1}", ++index, "Сасов Александр".D("Дмитрий Викторович Сасов"));
            //Console.WriteLine("{0}: {1}", ++index, "Иванов Иван Иванович".D("Иванов Иван Иванович"));

            
            CompareSimilarity(
                "Карасева Екатерина Викторовна".Eng()
                    , "Лазарева Екатерина Викторовна".Eng());
            CompareSimilarity("Мацуков Александр Сергеевич".Eng()
                , "Жуков Александр Сергеевич".Eng());
            CompareSimilarity("Викторов Иван Викторович"
                ,"Васильев Иван Викторович");
            CompareSimilarity("Попов Александр Александрович"
                ,"Долгов Александр Александрович");
            CompareSimilarity("Сасов Александр Викторович".Eng()
                , "Сасов Александр Виктрович".Eng());
            CompareSimilarity(
                "Сасов Александр Викторович".Eng()
                , "Sasov Alexander".ToLower());

            Console.ReadLine();
        }

        public static List<User> Users 
            = new List<User>();

        static void Load()
        {
            using (var sr = new StreamReader(@"D:\progress.csv"))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line.Length > 2)
                    {
                        Users.Add(new User(line));
                    }
                }
            }
        }
    
        static void Main1()
        {
            Load();

            Parallel.For(0, Users.Count - 1, Compare);
            
            //for(int from = 0; from < Users.Count; from++)
            //{
            //    var user = Users[from];
            //}
            Console.WriteLine("The End!");

        }

        static void Compare(int from)
        {
            //Logger.Info($"Index {from}" +
            //    $", {Thread.CurrentThread.ManagedThreadId}");

            var user = Users[from];
            for (int to = from + 1; to < Users.Count; to++)
            {
                var toUser = Users[to];

                var d = user.FIO.D(toUser.FIO);
                var fd = d / user.FIO.Length;
                if (d < 4)
                {
                    Logger.Info($"{user.FIO};{toUser.FIO};{d}");
                }

            }
        }
    }
}
