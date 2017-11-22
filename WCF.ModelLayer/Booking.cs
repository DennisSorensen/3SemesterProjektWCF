using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.ModelLayer
{
    public abstract class Booking
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string BookingType { get; set; }
        public int User_Id { get; set; }

        public Booking(int id, DateTime startDate, DateTime endDate, string bookingType, int user_Id)
        {
            this.Id = id;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.BookingType = bookingType;
            this.User_Id = user_Id;
        }
    }
}
