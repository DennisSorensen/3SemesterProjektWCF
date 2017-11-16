using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCF.ModelLayer;

namespace WCF.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICalendarService" in both code and config file together.
    [ServiceContract]
    public interface ICalendarService
    {
        [OperationContract]
        bool Create(Calendar calendar);
        [OperationContract]
        Calendar Get(int id);
        [OperationContract]
        void Edit(Calendar calendar);
        [OperationContract]
        void Delete(int id);
        [OperationContract]
        IEnumerable<Calendar> GetAll();

    }
}
