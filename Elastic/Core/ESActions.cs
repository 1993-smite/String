using Nest;

namespace Elastic.Core
{
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
