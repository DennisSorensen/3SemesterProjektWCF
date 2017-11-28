using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.DatabaseAccessLayer;
using WCF.ModelLayer;

namespace WCF.BusinessLogicLayer
{
    public class ReadyToGoController : IBookingController<ReadyToGo>
    {
        DataTable table = new DataTable();

        private ReadyToGoDb readyToGoDb;

        public ReadyToGoController()
        {
            readyToGoDb = new ReadyToGoDb();

        }

        public void Create(ReadyToGo readyToGo)
        {
            readyToGoDb.Create(readyToGo);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ReadyToGo Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReadyToGo> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReadyToGo> GetAllBookingForCalendar(int calendarId)
        {
            return readyToGoDb.GetAllBookingForCalendar(calendarId);
        }

        public void Update(ReadyToGo entity)
        {
            throw new NotImplementedException();
        }

        public DataTable CreateDatatable()
        {
            //Sorterer efter tid, 9 før 11 og dn 17 før 18

            return table;
        }
    }
}
