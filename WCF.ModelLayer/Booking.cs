using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCF.ModelLayer
{
    [DataContract]
    public abstract class Booking
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public string BookingType { get; set; }
        [DataMember]
        public int User_Id { get; set; }
        [DataMember]
        public int Calendar_Id { get; set; }


        public Booking(int id, DateTime startDate, DateTime endDate, string bookingType, int user_Id, int calendar_Id)
        {
            this.Id = id;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.BookingType = bookingType;
            this.User_Id = user_Id;
            this.Calendar_Id = calendar_Id;
        }
    }
}
