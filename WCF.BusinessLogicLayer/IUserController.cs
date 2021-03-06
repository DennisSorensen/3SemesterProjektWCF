﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.BusinessLogicLayer
{
    public interface IUserController<T>
    {
        bool Create(T entity); //Returnerer en bool
        void Update(T entity);
        void Delete(int id);
        IEnumerable<T> GetAll();
        T Get(int id);
    }
}
