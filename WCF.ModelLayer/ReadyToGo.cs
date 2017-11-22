using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.ModelLayer
{
    public class ReadyToGo : Booking
    {
       
        public string ProductNr { get; set; }
        public int AppendixNr { get; set; }
        public bool Contract { get; set; }

        public ReadyToGo(int id, DateTime startDate, DateTime endDate, string bookingType, int user_Id, string productNr, int appendixNr, bool contract) : base(id, startDate, endDate, bookingType, user_Id)
        {
            this.ProductNr = productNr;
            this.AppendixNr = appendixNr;
            this.Contract = contract;
        }

    }
}
