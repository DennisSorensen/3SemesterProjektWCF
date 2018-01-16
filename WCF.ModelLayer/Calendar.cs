using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCF.ModelLayer
{
    [DataContract]
    public class Calendar
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }

        public Calendar(int userId) //Id er autogenereret og inkrementeret i db, så derfor er den ikke med i constructor
        {
            this.UserId = userId; //Bruges til at vide hvilken person den tilhører
        }
    }
}
