using Practice.Interface;
using Practice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Repository
{
    public class BaseRepository<T>:IRepository<T> where T :class
    {
        public DbContext GetDbContextInstance()
        {
            return new PracticeContext();
        }
        protected void Batch(DbContext dbContext, EntityState state, params T[] entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            foreach (var entity in entities)
            {
                var dbEntry = dbContext.Entry<T>(entity);
                dbEntry.State = state;
            }
        }
        public int Insert(T entity)
        {
            using (var dbcontext = GetDbContextInstance())
            {
                Batch(dbcontext, EntityState.Added, entity);
                return dbcontext.SaveChanges();
            }
        }
        public int Insert(IEnumerable<T> entitys)
        {
            using (var dbcontext = GetDbContextInstance())
            {
                Batch(dbcontext, EntityState.Added, entitys.ToArray());
                return dbcontext.SaveChanges();
            }
        }
        /// <summary>
        /// 异步要用async await
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(IEnumerable<T> entitys)
        {
            using (var dbcontext = GetDbContextInstance())
            {
                Batch(dbcontext, EntityState.Added, entitys.ToArray());
                return await dbcontext.SaveChangesAsync();
            }
        }
        public T GetByKey(object obj)
        {
            using(var dbcontext = GetDbContextInstance())
            {
                return dbcontext.Set<T>().Find(obj);
            }
        }
        public IList<T> GetAll(System.Linq.Expressions.Expression<Func<T,bool>> predicate)
        {
            using (var dbcontext = GetDbContextInstance())
            {
                return dbcontext.Set<T>().Where(predicate).ToList();
            }
        }
    }
}
