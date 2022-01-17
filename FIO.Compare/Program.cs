﻿using NLog;
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

        static void Load()
        {
            using (var sr = new StreamReader(@"D:\progress1.csv"))
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

        static void Main(string[] args)
        {
            //Load();
            //Parallel.For(0, Users.Count - 1, Compare);

            var fio = "Иванов Иван";
            var fios = new List<string>()
            {
                "Иванов Иван Иванович",
                "Ивановский Антон Иванович",
                "Иванович Иван Антонович",
                "Иванов Артем Иванович"
            };


            foreach (var f in fios)
            {
                var d = fio.Eng().Metaphone(f.Eng());

                Console.WriteLine("\t {0}",d);
            }

            Console.WriteLine("Hello World!");
        }

        static void Compare(int from)
        {
            var user = Users[from];
            for (int to = from + 1; to < Users.Count; to++)
            {
                var toUser = Users[to];

                var d = user.FIO.Eng().Metaphone(toUser.FIO.Eng());
                if (d < 1)
                {
                    Logger.Info($"{user.FIO.Eng()};{toUser.FIO.Eng()};{d}");
                }

            }
        }
    }
}
