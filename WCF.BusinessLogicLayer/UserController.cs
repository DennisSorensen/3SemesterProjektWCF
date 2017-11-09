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
        public void Create(int id, string role, string password)
        {
            User user = new User(id, role, password);
            //Kalde db, og få den til at lægge den der i
        }

        public Tuple<int, string, string> Find(int id)
        {
            //Kalde db, og hente data

            //Sætte ting ind fra db
            User user = new User(1, "hej", "hej");
            
            return Tuple.Create(user.Id, user.Role, user.Password);
        }

    }
}
