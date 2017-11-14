using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.ModelLayer
{
    public class Calendar
    {
        public int Id { get; set; }
        public int UserId { get;}
        public int BookingId { get; set; }

        public Calendar(int id, int userId)
        {
            this.Id = id;
            this.UserId = userId;
        }
    }
}
