﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCF.BusinessLogicLayer;
using WCF.ModelLayer;

namespace WCF.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        private UserController userController = new UserController();

        public bool CreateUser(User user)
        {
            return userController.Create(user);
        }

        public User GetUser(int id)
        {
            return userController.Get(id);
        }
        
        public IEnumerable<User> GetAll()
        {
            return userController.GetAll();
        }

        public IEnumerable<User> GetAllSupporters()
        {
            return userController.GetAllSupporters();
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return userController.GetAllDepartments();
        }

        public IEnumerable<User> GetAllDepSupport(int id)
        {
            return userController.GetAllDepSupport(id);
        }

        public User Login(int id, string password)
        {
            return userController.Login(id, password);
        }
    }
}
