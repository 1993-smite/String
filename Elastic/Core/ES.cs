using Nest;
using System;
using System.Runtime.Caching;

namespace Elastic.Core
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
}
