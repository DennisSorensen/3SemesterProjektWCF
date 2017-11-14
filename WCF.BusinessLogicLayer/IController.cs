using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.BusinessLogicLayer
{
    public interface IController<T>
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
        IEnumerable<T> GetAll();
        T Get(int id);
    }
}
