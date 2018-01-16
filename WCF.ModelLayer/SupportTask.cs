using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCF.ModelLayer
{
    [DataContract]
    public class SupportTask : Booking //Den arver fra booking, man kan kun arve fra en klasse
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        
        public SupportTask(DateTime startDate, DateTime endDate, string bookingType, int user_Id, int calenar_Id, string name, string description) : base(startDate, endDate, bookingType, user_Id, calenar_Id) //Her bruger den consrtuctor fra booking til hjælp af oprettelse af supportTask.
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
