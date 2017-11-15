using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.ModelLayer;

namespace WCF.BusinessLogicLayer
{
    public class UserController
    {
        public void Create(User user)
        {

            //Kalde db, og få den til at lægge den der i
        }

        public User Get(int id)
        {
            //Kalde db, og hente data

            //Sætte ting ind fra db
            User user = new User(1, "Admin", "Bo", "Larsen", "Password");

            return user;
        }

    }
}
