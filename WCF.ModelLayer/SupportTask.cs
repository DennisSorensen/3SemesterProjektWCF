using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCF.ModelLayer
{
    [DataContract]
    public class SupportTask : Booking
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }

        public SupportTask(int id, DateTime startDate, DateTime endDate, string bookingType, int user_Id, int calenar_Id, string name, string description) : base(id, startDate, endDate, bookingType, user_Id, calenar_Id)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
