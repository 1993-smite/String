using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using StrignComporation;

namespace CheckFIO
{
    class Program
    {
        static List<string> LoadUsersSQL()
        {
            var fio = new List<string>();
            using (var sr = new StreamReader("D:\\users.csv"))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var arr = line.Split(';');

                    fio.Add(arr[2].Replace('"', ' ').Trim());
                }
            }

            return fio;
        }

        static List<string> LoadUsersProgress()
        {
            var fio = new List<string>();
            using (var sr = new StreamReader("D:\\progress.csv"))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var arr = line.Split(';');

                    fio.Add(arr[0].Trim());
                }
            }

            return fio;
        }

        static void AddLog(string log)
        {
            using (var sw = new StreamWriter("D:\\checkUsers3.csv", true))
            {
                sw.WriteLine(log);
            }
        }

        static void Main(string[] args)
        {
            var sql = LoadUsersSQL();
            var prog = LoadUsersProgress();

            int minIndex = 0;
            double cur, min = double.MaxValue;

            var sqlL = sql.OrderBy(x => x);

            var sw = new Stopwatch();
            sw.Start();
            foreach(var it in sqlL)
            {
                min = double.MaxValue;
                for (int i=0;i<prog.Count;i++)
                {
                    var it1 = prog[i];

                    cur = it.D(it1);
                    if (min > cur)
                    {
                        min = cur;
                        minIndex = i;
                    }
                }

                if (min > 0)
                    AddLog($"{it},{prog[minIndex]},{min}");
            }
            sw.Stop();
            Console.WriteLine("Time {0}", sw.ElapsedMilliseconds);
        }

        static void MainAsync(string[] args)
        {
            var sql = LoadUsersSQL();
            var prog = LoadUsersProgress();

            int minIndex = 0;
            double cur, min = double.MaxValue;

            var sqlL = sql.OrderBy(x => x);
            var tasks = new Task[prog.Count];

            var sw = new Stopwatch();
            sw.Start();
            foreach (var it in sqlL)
            {
                min = double.MaxValue;
                for (int i = 0; i < prog.Count; i++)
                {
                    object index = i;
                    var it1 = prog[i];

                    tasks[i] = new Task(() =>
                    {
                        cur = it.D(it1);
                        if (cur < min)
                        {
                            minIndex = (int)index;
                            min = cur;
                        }
                    });
                    tasks[i].Start();
                }
                Task.WaitAll(tasks);

                if (min > 0)
                    AddLog($"{it},{prog[minIndex]},{min}");
            }
            sw.Stop();

            Console.WriteLine("Time {0}", sw.ElapsedMilliseconds);
        }
    }
}
