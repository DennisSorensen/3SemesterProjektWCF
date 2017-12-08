using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Exceptions
{
    [DataContract]
    public class BookingExistsException
    {
        [DataMember]
        public string Message { get; set; }
        public BookingExistsException(string message)
        {
            Message = message;
        }
    }
}
