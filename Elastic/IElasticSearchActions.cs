using System;
using System.Collections.Generic;
using System.Text;

namespace Elastic
{
    interface IElasticSearchActions<T>
    {
        bool Create(T instance);
        bool Update(T instance);
        bool Delete(T instance);
    }
}
