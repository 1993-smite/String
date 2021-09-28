namespace Elastic
{
    interface IElasticActions<T>
    {
        bool Create(T instance);
        bool Update(T instance);
        bool Delete(T instance);
    }
}
