using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Interfaces
{
    public interface IRepository<T> where T : class
    {
        HttpResponseMessage Create(T entity);
        T Read(object id);
        List<T> GetAll();
        HttpResponseMessage Update(object id, T entity);
        HttpResponseMessage Delete(object id);
    }
}
