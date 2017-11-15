using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.DatabaseAccessLayer;
using WCF.ModelLayer;

namespace WCF.BusinessLogicLayer
{
    public class CalendarController : IController<Calendar>
    {
        private IDbCrud<Calendar> dbCRUD;

        public void Create(Calendar calendar)
        {
            dbCRUD.Create(calendar); 
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Calendar Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Calendar> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Calendar entity)
        {
            throw new NotImplementedException();
        }
    }
}
