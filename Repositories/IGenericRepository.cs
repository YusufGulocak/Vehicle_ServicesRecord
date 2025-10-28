using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories
{
   
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        ValueTask<T?> GetByIdAsync(int id);
        ValueTask AddAsync(T entity);
        void update (T entity);
        void  Delete(T entitiy  );
    }
}
