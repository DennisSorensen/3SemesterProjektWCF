using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCF.BusinessLogicLayer;
using WCF.ModelLayer;

namespace WCF.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BookingService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BookingService.svc or BookingService.svc.cs at the Solution Explorer and start debugging.
    public class BookingService : IBookingService
    {

        private ReadyToGoController readyToGoController = new ReadyToGoController();
        private SupportBookingController supportBookingController = new SupportBookingController();
        private TaskController taskController = new TaskController();
        private BookingController bookingController = new BookingController();

        public void CreateReadyToGo(ReadyToGo readyToGo)
        {
            readyToGoController.Create(readyToGo);
        }

        public void CreateSupportBooking(SupportBooking supportBooking)
        {
            supportBookingController.Create(supportBooking);
        }

        public void CreateSupportTask(SupportTask supportTask)
        {
            taskController.Create(supportTask);
        }

        public IEnumerable<Booking> GetAllBookingSpecificDay(int calendarId, DateTime date)
        {
            return bookingController.GetAllBookingSpecificDay(calendarId, date);
        }

        public IEnumerable<ReadyToGo> GetAllReadyToGo(int calendarId)
        {
            return readyToGoController.GetAllBookingForCalendar(calendarId);
        }

        public IEnumerable<SupportBooking> GetAllSupportBooking(int calendarId)
        {
            return supportBookingController.GetAllBookingForCalendar(calendarId);
        }

        public IEnumerable<SupportTask> GetAllSupportTask(int calendarId)
        {
            return taskController.GetAllBookingForCalendar(calendarId);
        }

        public Booking GetBooking(int bookingId)
        {
            return bookingController.GetBooking(bookingId);
        }

        public ReadyToGo GetReadyToGo(int id)
        {
            return readyToGoController.GetReadyToGo(id);
        }

        public SupportBooking GetSupportBooking(int id)
        {

            return supportBookingController.GetSupportBooking(id);
        }

        public SupportTask GetSupportTask(int id)
        {
            return taskController.GetSupportTask(id);
        }
    }
}
