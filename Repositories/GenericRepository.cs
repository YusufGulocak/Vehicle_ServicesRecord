using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories
{
    public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : class
    {
        protected AppDbContext Context = context;
        private readonly DbSet<T> _dbSet=context.Set<T>();
        public Task<Boolean> AnyAsync(int id )
        {
            return _dbSet.AnyAsync(predicate);
        }
        public async ValueTask AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public ValueTask<T?> GetByIdAsync(int id)
        {
            return _dbSet.FindAsync(id);
        }

        public void update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsNoTracking();
        }

    }
}
