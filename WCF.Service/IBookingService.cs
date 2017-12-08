using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCF.Exceptions;
using WCF.ModelLayer;

namespace WCF.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBookingService" in both code and config file together.
    [ServiceContract]
    public interface IBookingService
    {
        [OperationContract]
        [FaultContract(typeof(BookingExistsException))]
        void CreateSupportTask(SupportTask supportTask);

        [OperationContract]
        void CreateSupportBooking(SupportBooking supportBooking);

        [OperationContract]
        void CreateReadyToGo(ReadyToGo readyToGo);

        [OperationContract]
        SupportBooking GetSupportBooking(int id);

        [OperationContract]
        ReadyToGo GetReadyToGo(int id);

        [OperationContract]
        SupportTask GetSupportTask(int id);

        [OperationContract]
        Booking GetBooking(int bookingId);

        [OperationContract]
        IEnumerable<SupportTask> GetAllSupportTask(int calendarId);//Tjek om de bruges

        [OperationContract]
        IEnumerable<SupportBooking> GetAllSupportBooking(int calendarId);//Tjek om de bruges

        [OperationContract]
        IEnumerable<ReadyToGo> GetAllReadyToGo(int calendarId);//Tjek om de bruges

        [OperationContract]
        IEnumerable<Booking> GetAllBookingSpecificDay(int calendarId, DateTime date);

        [OperationContract]
        int FindAvaliableCalendar(DateTime startDate, DateTime endDate)
    }
}
