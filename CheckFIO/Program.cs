using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CheckFIO.Core;
using StrignComporation;

namespace CheckFIO
{
    class Program
    {
        #region Users

        static void AddLog(string log)
        {
            using (var sw = new StreamWriter("D:\\checkUsers3.csv", true))
            {
                sw.WriteLine(log);
            }
        }

        static void MainSync(string[] args)
        {
            var sql = FileSQLUsers.LoadUsersSQL();
            var prog = FileProgressUsers.LoadUsersProgress();

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

        static void Main(string[] args)
        {
            var sql = FileSQLUsers.LoadUsersSQL();
            var prog = FileProgressUsers.LoadUsersProgress();

            int minIndex = 0;
            double cur, min = double.MaxValue;

            var sqlL = sql.OrderBy(x => x);
            var tasks = new Task[prog.Count];

            foreach (var it in sqlL)
            {
                min = double.MaxValue;
                for (int i = 0; i < prog.Count; i++)
                {
                    var index = i;
                    var it1 = prog[i];

                    tasks[i] = new Task(() =>
                    {
                        cur = it.D(it1);
                        if (cur < min)
                        {
                            minIndex = index;
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

    }
}
