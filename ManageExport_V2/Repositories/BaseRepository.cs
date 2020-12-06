using ManageExport_V2.Models;
using ManageExport_V2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ManageExport_V2.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T: class
    {
        protected readonly ExportContext _context;
        private readonly DbSet<T> _dbSet;
        protected BaseRepository(ExportContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        #region

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual async Task Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public virtual async Task DeleteMulti(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _dbSet.Remove(obj);
            await Task.CompletedTask;
        }

        public virtual async Task<T> GetSingleById(int id)
        {
            return await Task.FromResult(_dbSet.Find(id));
        }

        public virtual async Task<IQueryable<T>> GetMany(Expression<Func<T, bool>> where, string includes)
        {
            return await Task.FromResult(_dbSet.Where(where).AsNoTracking());
        }

        public virtual async Task<int> Count(Expression<Func<T, bool>> where)
        {
            return await Task.FromResult(_dbSet.Count(where));
        }

        public async Task<IQueryable<T>> GetAll(string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Any())
            {
                var query = _dbSet.Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return await Task.FromResult(query.AsNoTracking());
            }

            return await Task.FromResult(_dbSet.AsNoTracking());
        }

        public async Task<T> GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Any())
            {
                var query = _dbSet.Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return await query.FirstOrDefaultAsync(expression);
            }
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public virtual async Task<IQueryable<T>> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            if (includes != null && includes.Any())
            {
                var query = _dbSet.Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return await Task.FromResult(query.Where<T>(predicate).AsNoTracking<T>());
            }

            return await Task.FromResult(_dbSet.Where<T>(predicate).AsNoTracking<T>());
        }

        public virtual async Task<IQueryable<T>> GetMultiPaging(Expression<Func<T, bool>> predicate, int index = 0, int size = 20, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> resetSet;

            if (includes != null && includes.Any())
            {
                var query = _dbSet.Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                resetSet = predicate != null ? _dbSet.Where<T>(predicate).AsQueryable() : _dbSet.AsQueryable();
            }

            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            //total = resetSet.Count();
            return await Task.FromResult(resetSet.AsNoTracking());
        }

        public async Task<bool> CheckContains(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(_dbSet.Count<T>(predicate) > 0);
        }

        #endregion
    }
}
