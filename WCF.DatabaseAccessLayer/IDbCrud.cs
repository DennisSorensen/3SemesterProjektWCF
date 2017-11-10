using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.DatabaseAccessLayer
{
    public interface IDbCrud<T>
    {
        void Create(T entity);
        T Get(int id);

    }
}
