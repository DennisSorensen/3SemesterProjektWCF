using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace WCF.ModelLayer
{
    [DataContract] //For at vi kan pakke dem sammen til xml og lave objekter af dem i fx klienten
    public class User
    {
        [DataMember] //Det er de fields som man skal kunne tilgå i klienten
        public int Id { get; set; }
        [DataMember]
        public string Role { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int DepartmentId { get; set; }

        public User(int id, string role,string firstName, string lastName, string password, int departmentId)
        {
            this.Id = id;
            this.Role = role;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.DepartmentId = departmentId;
        }
    }
}
