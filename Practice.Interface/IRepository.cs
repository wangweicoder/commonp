using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Interface
{
   public interface IRepository<T> where T:class
    {
        int Insert(T entity);
        int Insert(IEnumerable<T> entitys);
        Task<int> InsertAsync(IEnumerable<T> entitys);
        T GetByKey(object key);
        IList<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
    }
}
