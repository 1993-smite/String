using Elastic;
using Elastic.Core;
using NUnit.Framework;
using System;

namespace ElasticTest
{
    public class ESActionsTest
    {
        private Lazy<ESActions<Test>> _lazyActions = new Lazy<ESActions<Test>>(()=>new ESActions<Test>());
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

        [Test]
        [TestCase(100)]
        public void ESCreateTest(int id)
        {
            Actions.Create(getTest(id));
        }

        [Test]
        [TestCase(100)]
        public void ESUpdateTest(int id)
        {
            var test = getTest(id);
            test.Description = "updated";
            Actions.Update(test);
        }

        [Test]
        [TestCase(100)]
        public void ESDeleteTest(int id)
        {
            Actions.Delete(getTest(id));
        }
    }
}