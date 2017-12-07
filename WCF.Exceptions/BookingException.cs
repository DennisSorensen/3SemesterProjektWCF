using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Exceptions
{
    [DataContract]
    public class BookingException
    {
        [DataMember]
        public string Message { get; set; }
        public BookingException(string message)
        {
            Message = message;
        }
    }
}
