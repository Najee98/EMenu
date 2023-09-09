using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EMenu.Services.Interfaces
{
    public interface IGenericService<T>
    {
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? pageNumber = null,
        int? pageSize = null);

        T GetById(int id);
        T Create(T entity);
        T Update(T entity);
        void Delete(int id);
    }
}
