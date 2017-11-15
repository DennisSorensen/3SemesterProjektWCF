using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.DatabaseAccessLayer;
using WCF.ModelLayer;

namespace WCF.BusinessLogicLayer
{
    public class UserController : IController<User>
    {
        private IDbCrud<User> userDb; //Laver en instans af UserDb

        public UserController()
        {
            userDb = new UserDb(); //Initialisere userDb
        }

        public void Create(User user)
        {
            userDb.Create(user); //Kalder create user over i db, og lægger den medsendt user i db.
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            //Kalde db, og hente data

            //Sætte ting ind fra db
            User user = new User(1, "Admin", "Bo", "Larsen", "Password"); //Skal slettes og erstattes med rigtig user som kommer fra db

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
