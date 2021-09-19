using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StrignComporation;

namespace CheckFIO
{
    class Program
    {
        #region Users

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

        static void MainSync(string[] args)
        {
            var sql = LoadUsersSQL();
            var prog = LoadUsersProgress();

            int minIndex = 0;
            double cur, min = double.MaxValue;

            var sqlL = sql.OrderBy(x => x);

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
        }

        static void MainAsync(string[] args)
        {
            var sql = LoadUsersSQL();
            var prog = LoadUsersProgress();

            int minIndex = 0;
            double cur, min = double.MaxValue;

            var sqlL = sql.OrderBy(x => x);
            var tasks = new Task[prog.Count];

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
        }

        #endregion
        
        static Task Common(string str)
        {
            var tsk = new Task(() => Console.WriteLine("{0} {1} task {2}", str, Thread.CurrentThread.ManagedThreadId, Task.CurrentId));
            tsk.Start();
            return tsk;
        }

        static async Task<string> Test()
        {
            //await Common("Test");
            Console.WriteLine("Test {0} task {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            return "Test";
        }

        static async Task<string> Test1()
        {
            Console.WriteLine("Test1 {0} task {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            //await Common("Test1");
            //await new Task(() => Console.WriteLine("Test1 {0} task {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId));
            return "Test1";
        }

        static async Task<string> Test2()
        {
            await Common("Test2");
            Console.WriteLine("Test2 {0} task {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            return "Test2";
        }


        static async void MainTest(string[] args)
        {
            Console.WriteLine("MainTest begin {0} task {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            
            await Test();
            await Test1();
            await Test2();

            Console.WriteLine("MainTest end {0} task {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
        }

        static void Main(string[] args)
        {
            var tsk = new Task(() => MainTest(args));
            tsk.Start();

            Console.ReadLine();
        }

    }
}
