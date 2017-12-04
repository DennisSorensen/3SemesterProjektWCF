using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.DatabaseAccessLayer;
using WCF.ModelLayer;

namespace WCF.BusinessLogicLayer
{
    public class SupportBookingController : IBookingController<SupportBooking>
    {
        private SupportBookingDb supportBookingDb;

        public SupportBookingController()
        {
            supportBookingDb = new SupportBookingDb();
        }

        public void Create(SupportBooking supportBooking)
        {
            supportBookingDb.Create(supportBooking);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public SupportBooking GetSupportBooking(int id)
        {
            return supportBookingDb.Get(id);
        }

        public IEnumerable<SupportBooking> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SupportBooking> GetAllBookingForCalendar(int calendarId)
        {
            return supportBookingDb.GetAllBookingForCalendar(calendarId);
        }

        public void Update(SupportBooking entity)
        {
            throw new NotImplementedException();
        }

       }
}
