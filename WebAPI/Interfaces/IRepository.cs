using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface IRepository<T> where T: class
    {
        T Create(T entity);
        T Read(Guid id);
        List<T> GetAll();
        T Update(Guid id, T entity);
        Guid Delete(Guid id);
    }
}
