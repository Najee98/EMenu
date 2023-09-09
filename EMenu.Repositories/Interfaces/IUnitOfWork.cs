using EMenu.Repositories.Implementaiton;
using EMenu.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMenu.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        GenericRepository<T> GenericRepository<T>() where T : class;
        void save();
    }
}
