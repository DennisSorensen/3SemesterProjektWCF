using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCF.ModelLayer
{
    [DataContract]
    public class ReadyToGo : Booking
    {
        [DataMember]
        public string ProductNr { get; set; }
        [DataMember]
        public int AppendixNr { get; set; }
        [DataMember]
        public bool Contract { get; set; }

        public ReadyToGo(DateTime startDate, DateTime endDate, string bookingType, int user_Id, int calendar_Id, string productNr, int appendixNr, bool contract) : base(startDate, endDate, bookingType, user_Id, calendar_Id)
        {
            this.ProductNr = productNr;
            this.AppendixNr = appendixNr;
            this.Contract = contract;
        }

    }
}
