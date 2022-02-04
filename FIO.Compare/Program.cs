using NLog;
using StrignComporation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FIO.Compare
{
    class Program
    {
        public static Logger Logger =
            LogManager.Setup().GetCurrentClassLogger();

        public static List<User> Users
            = new List<User>();

        public static int Cnt = 0;

        static void Load()
        {
            using (var sr = new StreamReader(@"D:\progress4.csv"))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line.Length > 2)
                    {
                        Users.Add(new User(line.ToLower().Filter().Replace("?", "")));
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Load();
            Parallel.For(0, Users.Count - 1, Compare);

            //Console.WriteLine("test".IsEnglish());
            //Console.WriteLine("test ggg".IsEnglish());
            //Console.WriteLine("test9".IsEnglish());
            //Console.WriteLine("апра".IsEnglish());
            //Console.WriteLine("апра авпв".IsEnglish());
            //Console.WriteLine("апра efgdf".IsEnglish());

            Console.WriteLine("The End!");
        }

        static void Compare(int from)
        {
            Cnt++;

            if (Cnt % 100 == 0)
                Console.WriteLine(Cnt);

            var user = Users[from];
            for (int to = from + 1; to < Users.Count; to++)
            {
                var toUser = Users[to];

                if (user.FIO.Eng().FuzzyCompare(toUser.FIO.Eng()))
                {
                    Logger.Info($"{from};{user.FIO};{to};{toUser.FIO}");
                }

            }
        }
    }
}
