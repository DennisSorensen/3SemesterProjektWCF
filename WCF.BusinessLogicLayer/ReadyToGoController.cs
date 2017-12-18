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

        public ReadyToGo GetReadyToGo(int id)
        {
            return readyToGoDb.Get(id);
        }

        public IEnumerable<ReadyToGo> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReadyToGo> GetAllBookingForCalendar(int calendarId)
        {
            return readyToGoDb.GetAllBookingForCalendar(calendarId);
        }

        public void Update(ReadyToGo readyToGo)
        {
            throw new NotImplementedException();
        }
        
    }
}
