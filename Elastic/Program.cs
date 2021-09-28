using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Elastic.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Elastic
{
    public class ESActionsTest
    {
        private Lazy<ESActions<Test>> _lazyActions = new Lazy<ESActions<Test>>(() => new ESActions<Test>());
        public ESActions<Test> Actions => _lazyActions.Value;

        private Test getTest(int id)
        {
            id = id < 10000 ? id + 10000 : id;

            return new Test()
            {
                Id = id,
                Title = $"title {id}",
                Description = $"description {id}"
            };
        }

        [Benchmark]
        public void ESCreateTest()
        {
            Actions.Create(getTest(101));
        }

        [Benchmark]
        public void ESUpdateTest()
        {
            Actions.Create(getTest(101));
        }

        [Benchmark]
        public void ESDeleteTest()
        {
            Actions.Delete(getTest(101));
        }
    }

    class Program
    {
        static ESActions<Test> Actions => new ESActions<Test>();

        static void BenchmarkRunTestActionTime()
        {
            BenchmarkRunner.Run<ESActionsTest>();
        }

        static Test ESActionCreate()
        {
            var test = new Test()
            {
                Id = 9,
                Title = "c# .net auto create",
                Description = "auto create elastic search"
            };
            Actions.Create(test);
            Console.WriteLine($"Result: created \n{test}");

            return test;
        }

        static Test ESActionUpdate(Test test)
        {
            test.Title = "c# .net auto update";

            Actions.Update(test);
            Console.WriteLine($"Result: updated \n{test}");

            return test;
        }

        static void ESActionDelete(Test test)
        {
            Actions.Delete(test);
            Console.WriteLine($"Result: removed \n{test}");
        }

        static void Main(string[] args)
        {
            var tsk = new Task<Test>(() => ESActionCreate());
            tsk.ContinueWith((tsk) => ESActionUpdate(tsk.Result))
               .ContinueWith((tsk) => ESActionDelete(tsk.Result));
            tsk.Start();

            BenchmarkRunTestActionTime();

            Console.ReadLine();
        }
    }
}
