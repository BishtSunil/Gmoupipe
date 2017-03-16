using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.DataRepository.Infrastructure
{
    public abstract class GenericRepository<C, T> :
         IGenericRepository<T>
        where T : class

        where C : DbContext, new()
    {

        private C _entities = new C();



        public C Context
        {

            get { return _entities; }
            set { _entities = value; }
        }

        public virtual IQueryable<T> GetAll()
        {

            IQueryable<T> query = _entities.Set<T>();
            return query;
        }

        public T FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            T query = _entities.Set<T>().Where(predicate).FirstOrDefault();

            return query;
        }

        public virtual string Add(T entity)
        {
            _entities.Set<T>().Add(entity);

            return string.Empty;

        }

        public virtual string Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
            return string.Empty;
        }

        public virtual string Edit(T entity)
        {

            _entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return string.Empty;
        }

        public virtual string Save()
        {
            try
            {
                _entities.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
            return string.Empty;

        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {

            if (!this.disposed)
                if (disposing)
                    _entities.Dispose();

            this.disposed = true;
        }

        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
