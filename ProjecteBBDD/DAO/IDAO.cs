using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjecteBBDD.DAO
{

    public interface IDAO<T, K>
    {
        void Insert(T entity);
        int InsertReturnID(T entity);
        void Update(T entity);
        void Delete(T entity);
        T SelectById(K id);
        List<T> SelectAll();
    }
}
