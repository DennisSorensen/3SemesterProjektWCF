using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.ModelLayer
{
    public class SupportBooking : Booking
    {
        

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Phone { get; set; }
        public string Description { get; set; }

        public SupportBooking(int id, DateTime startDate, DateTime endDate, string bookingType, int user_Id, int calendar_Id, string firstName, string lastName, int phone, string description) : base(id, startDate, endDate, bookingType, user_Id, calendar_Id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone;
            this.Description = description;
        }

    }
}
