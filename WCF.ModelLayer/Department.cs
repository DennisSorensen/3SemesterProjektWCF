using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCF.ModelLayer
{ //Den bliver lavet i db, man kan ikke oprettes den på nogen måde ud over i db
    [DataContract]
    public class Department
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }

        public Department(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
