using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.ModelLayer
{
    public class User
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public User(int id, string role,string name, string password)
        {
            this.Id = id;
            this.Role = role;
            this.Name = name;
            this.Password = password;
        }
    }
}
