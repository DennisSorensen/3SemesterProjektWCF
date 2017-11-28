using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace WCF.ModelLayer
{
    [DataContract]
    public class User
    {
        [DataMember]
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
