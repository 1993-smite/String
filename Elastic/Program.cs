using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Elastic.Core;
using System;
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
        static void BenchmarkRunTestActionTime()
        {
            var summary = BenchmarkRunner.Run<ESActionsTest>();
        }

        static void ESActionCreate()
        {
            var test = new Test();
            var response = ES
                .ESClient
                .Get<Test>(1, idx => idx.Index(test.GetIndex)); // returns an IGetResponse mapped 1-to-1 with the Elasticsearch JSON response
            test = response.Source; // the original document

            var actions = new ESActions<Test>();
            actions.Create(new Test()
            {
                Id = 9,
                Title = "c# .net auto create",
                Description = "auto create elastic search"
            });
            Console.WriteLine($"Result: \n{test}");
        }

        static void Main(string[] args)
        {
            var tsk = new Task(ESActionCreate);
            tsk.Start();

            BenchmarkRunTestActionTime();

            Console.ReadLine();
        }
    }
}
