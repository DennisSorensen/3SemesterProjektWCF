using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.DatabaseAccessLayer;
using WCF.ModelLayer;

namespace WCF.BusinessLogicLayer
{
    public class BookingController
    {
        private BookingDb bookingDb;

        public BookingController()
        {
            bookingDb = new BookingDb();
        }

        public Booking GetBooking(int bookingId)
        {
            return bookingDb.GetBooking(bookingId);
        }

        public IEnumerable<Booking> GetAllBookingSpecificDay(int calendarId, DateTime date)
        {
            return bookingDb.GetAllBookingSpecificDay(calendarId, date);
        }
        public int FindAvaliableCalendar(DateTime startDate, DateTime endDate)
        {
            return bookingDb.FindAvaliableCalendar(startDate, endDate);
        }
    }
}
