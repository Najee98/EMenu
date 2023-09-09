using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMenu.Repositories.Implementaiton
{
    using EMenu.Repositories.Implementaiton;
    using EMenu.Repositories.Interfaces;
    using EMenu.Repositories;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            IGenericRepository<T> repo = new GenericRepository<T>(_context);
            return repo;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        GenericRepository<T> IUnitOfWork.GenericRepository<T>()
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            throw new NotImplementedException();
        }

    }
}
