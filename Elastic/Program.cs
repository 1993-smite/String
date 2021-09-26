using Elasticsearch.Net;
using Nest;
using System;

namespace Elastic
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Test();
            //var response = ES.ESClient.Get<Test>(1, idx => idx.Index(test.GetIndex)); // returns an IGetResponse mapped 1-to-1 with the Elasticsearch JSON response
            //test = response.Source; // the original document

            var actions = new ESTestActions<Test>();
            actions.Create(new Test()
            {
                Id = 7,
                Title = "c# .net auto create",
                Description = "auto create elastic search"
            });

            Console.WriteLine($"Result: \n{test}");
        }
    }
}
