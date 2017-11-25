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
        private TaskController taskController = new TaskController();
        private SupportBookingController supportBookingController = new SupportBookingController();
        private ReadyToGoController readyToGoController = new ReadyToGoController();
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

        public SupportBooking GetSupportBooking(int id)
        {

            return supportBookingController.Get(id);
        }
        
    }
}
