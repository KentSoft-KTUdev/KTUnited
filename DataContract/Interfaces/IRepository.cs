using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Boolean Create(T entity);
        T Read(object id);
        List<T> GetAll();
        Boolean Update(object id, T entity);
        Boolean Delete(object id);
    }
}
