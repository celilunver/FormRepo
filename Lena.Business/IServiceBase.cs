using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lena.Business
{
    public interface IServiceBase<T> where T : class
    {
        List<T> getAll();
        T add(T entity);
        T update(T entity);
        T get(int entityId);
        bool delete(int entityId);
    }
}
