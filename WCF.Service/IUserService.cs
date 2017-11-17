using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCF.ModelLayer;

namespace WCF.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserService" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {

        [OperationContract]
        bool CreateUser(User user);

        [OperationContract]
        User GetUser(int id);

        [OperationContract]
        IEnumerable<User> GetAll();

        [OperationContract]
        IEnumerable<User> GetAllSupporters();
    }
}
