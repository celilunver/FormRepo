using Lena.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lena.Data.UnitofWork
{
    public class UnitofWork : IUnitofWork
    {
        private readonly LenaDbEntities2 context;
        private Dictionary<Type, object> repositories;

        public UnitofWork(LenaDbEntities2 _context)
        {
            context = _context;
            repositories = repositories ?? new Dictionary<Type, object>();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (repositories.Keys.Contains(typeof(T)))
            {
                return repositories[typeof(T)] as IRepository<T>;
            }

            var repository = new RepositoryBase<T>(context);
            repositories.Add(typeof(T), repository);

            return repository;
        }

        public virtual int SaveChanges()
        {
            return context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
