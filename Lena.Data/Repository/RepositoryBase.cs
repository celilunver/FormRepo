using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lena.Data.Repository
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly LenaDbEntities context;
        public RepositoryBase(LenaDbEntities context)
        {
            this.context = context;
        }
        protected virtual IQueryable<T> GetQueryable(Expression<Func<T, bool>> filter = null,
                                                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                     Expression<Func<T, object>> include = null,
                                                     int? skip = null,
                                                     int? take = null)
        {
            IQueryable<T> query = context.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (include != null)
            {
                query = query.Include(include);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }
            return query;
        }

        public virtual IQueryable<T> GetQueryable()
        {
            return context.Set<T>();
        }
        public virtual IQueryable<T> GetAll()
        {
            return Get(null, null, null, null);
        }
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> filter = null,
                                         Expression<Func<T, object>> include = null,
                                         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                         int? skip = null,
                                         int? take = null)
        {
            return GetQueryable(filter, orderBy, include, skip, take);
        }
        public virtual T Get(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> include = null)
        {
            return GetQueryable(filter, null, include).SingleOrDefault();
        }

        public virtual T Get(Expression<Func<T, bool>> filter = null)
        {
            return GetQueryable(filter, null, null).SingleOrDefault();
        }
        public virtual int Count(Expression<Func<T, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }
        public virtual T Add(T entity)
        {
            return context.Set<T>().Add(entity);
        }
        public virtual void Update(T entity)
        {
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            var dbSet = context.Set<T>();
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public int SqlQueryExecute(string sql, params object[] parameters)
        {
            return context.Database.ExecuteSqlCommand(sql, parameters);
        }

        public IEnumerable<T> SqlQueryExecuteForT(string sql, params object[] parameters)
        {
            return context.Database.SqlQuery<T>(sql, parameters).AsEnumerable();
        }

        public virtual IQueryable<T> Get(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>();
            foreach (var include in includes)
            {
                query.Include(include.Name);
            }
            return query;
        }
    }
}
