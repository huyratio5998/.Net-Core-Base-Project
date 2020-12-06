using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ManageExport_V2.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T: class
    {
        void Add(T entity);

        Task Update(T entity);

        void Delete(T entity);

        void Delete(int id);

        Task DeleteMulti(Expression<Func<T, bool>> where);

        Task<T> GetSingleById(int id);

        Task<T> GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        Task<IQueryable<T>> GetAll(string[] includes = null);

        Task<IQueryable<T>> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);

        Task<IQueryable<T>> GetMultiPaging(Expression<Func<T, bool>> filter, int index = 0, int size = 50, string[] includes = null);

        Task<int> Count(Expression<Func<T, bool>> where);

        Task<bool> CheckContains(Expression<Func<T, bool>> predicate);
    }
}
