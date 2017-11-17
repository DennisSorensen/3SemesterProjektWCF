using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.DatabaseAccessLayer;
using WCF.ModelLayer;

namespace WCF.BusinessLogicLayer
{
    public class CalendarController : ICalendarController<Calendar>
    {
        private IDbCrud<Calendar> dbCRUD;

        public CalendarController()
        {
            dbCRUD = new CalendarDb();
        }

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
            return dbCRUD.Get(id);
        }

        public IEnumerable<Calendar> GetAll()
        {
            return dbCRUD.GetAll();
        }

        public void Update(Calendar entity)
        {
            throw new NotImplementedException();
        }
    }
}
