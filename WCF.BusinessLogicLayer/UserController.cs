using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.DatabaseAccessLayer;
using WCF.ModelLayer;

namespace WCF.BusinessLogicLayer
{
    public class UserController : IUserController<User>
    {
        private UserDb userDb; //Laver en instans af UserDb

        public UserController()
        {
            userDb = new UserDb(); //Initialisere userDb
        }

        public bool Create(User user)
        {
            bool notFoundUserWithSameId = true;

            if (userDb.Get(user.Id) == null)
            {
                userDb.Create(user); //Kalder create user over i db, og lægger den medsendt user i db.
            }
            else
            {
                notFoundUserWithSameId = false;
            }

            return notFoundUserWithSameId;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            return userDb.Get(id);
        }

        public IEnumerable<User> GetAll()
        {
            return userDb.GetAll();
        }

        public IEnumerable<User> GetAllSupporters()
        {
            return userDb.GetAllSupporters();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return userDb.GetAllDepartments();
        }

        public IEnumerable<User> GetAllDepSupport(int id)
        {
            return userDb.GetAllDepSupport(id);

        }

        public User Login(int id, string password)
        {
            return userDb.Login(id, password);
        }
    }
}
