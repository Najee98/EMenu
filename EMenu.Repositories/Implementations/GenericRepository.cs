using EMenu.Repositories.Interfaces;
using EMenu.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EMenu.Repositories.Implementaiton
{

    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public async Task<T> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            return entity;
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public async Task<T> DeleteAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<T> AddAsynch(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsynch(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsynch(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
