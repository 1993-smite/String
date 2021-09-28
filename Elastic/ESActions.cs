using Nest;
using System;
using System.Runtime.Caching;

namespace Elastic
{
    public static class ES
    {
        private static ElasticClient getClient()
        {
            var node = new Uri("http://localhost:9200/");
            var settings = new ConnectionSettings(node);
            return new ElasticClient(settings);
        }

        private static ElasticClient getCachedClient()
        {
            ObjectCache cache = MemoryCache.Default;

            var esClient = cache.Get("ESClient");
            ElasticClient client;
            if (esClient == null)
            {
                client = getClient();
            }
            else
            {
                client = (ElasticClient)esClient;
            }

            return client;
        }

        public static ElasticClient ESClient => getCachedClient();
    } 

    public class ESActions<T_ESModel> : IElasticActions<T_ESModel> where T_ESModel : ESModel
    {
        public bool Create(T_ESModel instance)
        {
            var result = ES.ESClient
                .Create<T_ESModel>(
                    instance
                    , (x) => x
                        .Index(instance.GetIndex)
                        .Id(instance.Id));
            return result.IsValid;
        }
        public bool Update(T_ESModel instance)
        {
            var result = ES.ESClient
                .Update<T_ESModel, object>(DocumentPath<T_ESModel>
                    .Id(instance.Id)
                    , (x)=>x
                        .Index(instance.GetIndex)
                        .Doc((object)instance));
            return result.IsValid;
        }
        public bool Delete(T_ESModel instance)
        {
            var result = ES.ESClient
                .Delete<T_ESModel>(DocumentPath<T_ESModel>
                    .Id(instance.Id)
                    , x=>x.Index(instance.GetIndex));
            return result.IsValid;
        }
    }
}
