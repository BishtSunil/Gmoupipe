using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.DataRepository.Infrastructure
{
    public interface IGenericRepository<T>
        where T : class
    {
        IQueryable<T> GetAll();
        T FindBy(Expression<Func<T, bool>> predicate);
        string Add(T entity);
        string Delete(T entity);
        string Edit(T entity);
        string Save();

    }
}
