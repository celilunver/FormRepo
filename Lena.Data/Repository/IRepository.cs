using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lena.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Get(Expression<Func<T, bool>> filter = null,
                          Expression<Func<T, object>> include = null,
                          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                          int? skip = null,
                          int? take = null);
        IQueryable<T> Get(params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAll();
        T Get(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> include = null);

        T Get(Expression<Func<T, bool>> filter = null);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int SqlQueryExecute(string command, params object[] parameters);
        IEnumerable<T> SqlQueryExecuteForT(string command, params object[] parameters);
    }
}
