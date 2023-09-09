using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EMenu.Repositories.Interfaces
{
    public interface IGenericRepository<T> : IDisposable
    {
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        T GetById(Object id);
        Task<T> GetByIdAsync(Object id);
        void Add(T entity);
        Task<T> AddAsynch(T entity);
        void Update(T entity);
        Task<T> UpdateAsynch(T entity);
        void Delete(T entity);
        Task<T> DeleteAsynch(T entity);
    }
}
